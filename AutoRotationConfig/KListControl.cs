
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace AutoRotationConfig
{

    /// <summary>
    /// A Kinetic list control.
    /// </summary>
    public class KListControl : Control, IEnumerable
    {
        bool drawSeparators = false;

        [DefaultValue(false)]
        public bool DrawSeparators
        {
            get { return drawSeparators; }
            set { drawSeparators = value; }
        }

        /// <summary>
        /// Interface for items contained within the list.
        /// </summary>
        public interface IKListItem
        {
            /// <summary>
            /// Gets or sets the parent.
            /// </summary>
            /// <value>The parent.</value>
            KListControl Parent { get; set; }

            /// <summary>
            /// The unscrolled bounds for this item.
            /// </summary>
            Rectangle Bounds { get; set; }

            /// <summary>
            /// Gets or sets the X.
            /// </summary>
            /// <value>The X.</value>
            int XIndex { get; set; }

            /// <summary>
            /// Gets or sets the Y.
            /// </summary>
            /// <value>The Y.</value>
            int YIndex { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="IKListItem"/> is selected.
            /// </summary>
            /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
            bool Selected { get; set; }

            /// <summary>
            /// Gets or sets the text.
            /// </summary>
            /// <value>The text.</value>
            string Text { get; set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            object Value { get; set; }

            /// <summary>
            /// Renders the specified graphics object.
            /// </summary>
            /// <param name="g">The graphics.</param>
            /// <param name="bounds">The bounds.</param>
            void Render(Graphics g, Rectangle bounds);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KListControl"/> class.
        /// </summary>
        public KListControl()
        {
            this.ParentChanged += new EventHandler(KListControl_Initialize);
            m_timer.Interval = 10;
            m_timer.Tick += new EventHandler(m_timer_Tick);
        }

        void KListControl_Initialize(object sender, EventArgs e)
        {
            CreateBackBuffer();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"></see> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            CleanupBackBuffer();

            m_timer.Enabled = false;

            base.Dispose(disposing);
        }

        /// <summary>
        /// Occurs when the selected item changes.
        /// </summary>
        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Occurs when the selected item is clicked on (after already being selected).
        /// </summary>
        public event EventHandler SelectedItemClicked;

        /// <summary>
        /// Gets the <see cref="Scroller.KListControl.IKListItem"/> at the specified index.
        /// </summary>
        public IKListItem this[int index]
        {
            get
            {
                return m_items[0][index];
            }
        }

        /// <summary>
        /// Gets the <see cref="Scroller.KListControl.IKListItem"/> at the specified index.
        /// </summary>
        public IKListItem this[int x, int y]
        {
            get
            {
                return m_items[x][y];
            }
        }

        /// <summary>
        /// The selected index.
        /// </summary>
        public Point SelectedIndex
        {
            get
            {
                return m_selectedIndex;
            }
        }

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public IKListItem SelectedItem
        {
            get
            {
                return m_selectedItem;
            }
        }

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                if (m_items.Count == 0)
                    return 0;
                else
                    return m_items[0].Count;
            }
        }

        /// <summary>
        /// Gets or sets the maximum scroll velocity.
        /// </summary>
        /// <value>The maximum velocity.</value>
        public int MaxVelocity
        {
            get
            {
                return m_maxVelocity;
            }
            set
            {
                m_maxVelocity = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of items in the control.
        /// </summary>
        /// <value>The height of the items.</value>
        public int ItemHeight
        {
            get
            {
                // In horizontal mode, we just use the full bounds, other modes use m_itemHeight.
                return m_layout == KListLayout.Horizontal ? Bounds.Height : m_itemHeight;
            }
            set
            {
                m_itemHeight = value;
                Reset();
            }
        }

        /// <summary>
        /// Gets or sets the height of items in the control.
        /// </summary>
        /// <value>The height of the items.</value>
        public int ItemWidth
        {
            get
            {
                // In vertical mode, we just use the full bounds, other modes use m_itemWidth.
                return m_layout == KListLayout.Vertical ? Bounds.Width : m_itemWidth;
            }
            set
            {
                m_itemWidth = value;
                Reset();
            }
        }

        /// <summary>
        /// Gets or sets the layout.
        /// </summary>
        /// <value>The layout.</value>
        public KListLayout Layout
        {
            get
            {
                return m_layout;
            }
            set
            {
                m_layout = value;
            }
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="text">The text for the item.</param>
        /// <param name="value">A value related to the item.</param>
        public void AddItem(string text, object value)
        {
            if (m_layout == KListLayout.Grid)
            {
                throw new NotSupportedException("List is not in grid mode");
            }

            IKListItem item = new AppListItem();
            item.Text = text;
            item.Value = value;
            item.XIndex = m_layout == KListLayout.Vertical ? 0 : m_items.Count;
            item.YIndex = m_layout == KListLayout.Horizontal ? 0 :
                m_items.ContainsKey(0) ? m_items[0].Count : 0;
            AddItem(item);
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="text">The text for the item.</param>
        /// <param name="value">A value related to the item.</param>
        public void AddItem(int x, int y, string text, object value)
        {
            if (m_layout != KListLayout.Grid)
            {
                throw new NotSupportedException("List is in grid mode");
            }

            AppListItem item = new AppListItem();
            item.Text = text;
            item.Value = value;
            item.XIndex = x;
            item.YIndex = y;
            AddItem(item);
        }

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(IKListItem item)
        {
            item.Parent = this;
            item.Selected = false;
            if (!m_items.ContainsKey(item.XIndex))
            {
                m_items.Add(item.XIndex, new ItemList());
            }
            item.Bounds = ItemBounds(item.XIndex, item.YIndex);
            m_items[item.XIndex].Add(item.YIndex, item);
            Reset();
        }

        /// <summary>
        /// Removes an item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveItem(IKListItem item)
        {
            if (m_items.ContainsKey(item.XIndex) &&
                m_items[item.XIndex].ContainsKey(item.YIndex))
            {
                m_items[item.XIndex].Remove(item.YIndex);
            }
            Reset();
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            m_items.Clear();
            Reset();
        }

        /// <summary>
        /// Invalidates the item (when visible).
        /// </summary>
        /// <param name="item">The item.</param>
        public void Invalidate(IKListItem item)
        {
            Rectangle itemBounds = item.Bounds;
            itemBounds.Offset(-m_offset.X, -m_offset.Y);
            if (Bounds.IntersectsWith(itemBounds))
            {
                Invalidate(itemBounds);
            }
        }

        /// <summary>
        /// Begins updates - suspending layout recalculation.
        /// </summary>
        public void BeginUpdate()
        {
            m_updating = true;
        }

        /// <summary>
        /// Ends updates - re-enabling layout recalculation.
        /// </summary>
        public void EndUpdate()
        {
            m_updating = false;
            Reset();
        }

        /// <summary>
        /// Called when the control is resized.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CreateBackBuffer();
            Reset();
        }

        /// <summary>
        /// Handles the Tick event of the m_timer control.
        /// </summary>
        private void m_timer_Tick(object sender, EventArgs e)
        {
            if (!Capture && (m_velocity.Y != 0 || m_velocity.X != 0))
            {
                m_offset.Offset(m_velocity.X, m_velocity.Y);

                ClipScrollPosition();

                // Slow down
                if (((++m_timerCount) % 10) == 0)
                {
                    if (m_velocity.Y < 0)
                    {
                        m_velocity.Y++;
                    }
                    else if (m_velocity.Y > 0)
                    {
                        m_velocity.Y--;
                    }
                    if (m_velocity.X < 0)
                    {
                        m_velocity.X++;
                    }
                    else if (m_velocity.X > 0)
                    {
                        m_velocity.X--;
                    }
                }

                if (m_velocity.Y == 0 && m_velocity.X == 0)
                {
                    m_timer.Enabled = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do nothing
        }

        /// <summary>
        /// Paints the control.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_backBuffer != null)
            {
                m_backBuffer.Clear(BackColor);

                Point startIndex = FindIndex(Bounds.Left, Bounds.Top);

                GridList.Enumerator xEnumerator = m_items.GetEnumerator();
                bool moreX = xEnumerator.MoveNext();
                while (moreX && xEnumerator.Current.Key < startIndex.X)
                {
                    moreX = xEnumerator.MoveNext();
                }

                while (moreX)
                {
                    ItemList yList = xEnumerator.Current.Value;
                    if (yList != null)
                    {
                        ItemList.Enumerator yEnumerator = yList.GetEnumerator();
                        bool moreY = yEnumerator.MoveNext();
                        while (moreY && yEnumerator.Current.Key < startIndex.Y)
                        {
                            moreY = yEnumerator.MoveNext();
                        }

                        while (moreY)
                        {
                            IKListItem item = yEnumerator.Current.Value;
                            if (item != null)
                            {
                                Rectangle itemRect = item.Bounds;
                                itemRect.Offset(-m_offset.X, -m_offset.Y);
                                if (Bounds.IntersectsWith(itemRect))
                                {
                                    if (this.DrawSeparators)
                                    {
                                        using (Pen whitePen = new Pen(ForeColor))
                                        {
                                            if (m_layout == KListLayout.Vertical || m_layout == KListLayout.Grid)
                                            {
                                                m_backBuffer.DrawLine(whitePen, itemRect.Left, itemRect.Top, itemRect.Right, itemRect.Top);
                                                m_backBuffer.DrawLine(whitePen, itemRect.Left, itemRect.Bottom, itemRect.Right, itemRect.Bottom);
                                            }
                                            if (m_layout == KListLayout.Horizontal || m_layout == KListLayout.Grid)
                                            {
                                                m_backBuffer.DrawLine(whitePen, itemRect.Left, itemRect.Top, itemRect.Left, itemRect.Bottom);
                                                m_backBuffer.DrawLine(whitePen, itemRect.Right, itemRect.Top, itemRect.Right, itemRect.Bottom);
                                            }
                                        }
                                    }
                                    item.Render(m_backBuffer, itemRect);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            moreY = yEnumerator.MoveNext();
                        }
                    }

                    moreX = xEnumerator.MoveNext();
                }

                e.Graphics.DrawImage(m_backBufferBitmap, 0, 0);
            }
            else
            {
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// Called when the user clicks on the control with the mouse.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Capture = true;

            m_mouseDown.X = e.X;
            m_mouseDown.Y = e.Y;
            m_mousePrev = m_mouseDown;
        }

        /// <summary>
        /// Called when the user moves the mouse over the control.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                Point currPos = new Point(e.X, e.Y);

                int distanceX = m_mousePrev.X - currPos.X;
                int distanceY = m_mousePrev.Y - currPos.Y;

                m_velocity.X = distanceX / 2;
                m_velocity.Y = distanceY / 2;
                ClipVelocity();

                m_offset.Offset(distanceX, distanceY);
                ClipScrollPosition();

                m_mousePrev = currPos;

                Invalidate();
            }
        }

        /// <summary>
        /// Called when the user releases a mouse button.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Did the click end on the same item it started on?
            bool sameX = Math.Abs(e.X - m_mouseDown.X) < m_itemWidth;
            bool sameY = Math.Abs(e.Y - m_mouseDown.Y) < m_itemHeight;

            if ((m_layout == KListLayout.Vertical && sameY) ||
                (m_layout == KListLayout.Horizontal && sameX) ||
                (m_layout == KListLayout.Grid && sameX && sameY))
            {
                // Yes, so select that item.
                Point selectedIndex = FindIndex(e.X, e.Y);
                if (selectedIndex != m_selectedIndex)
                {
                    IKListItem item = null;
                    if (m_items.ContainsKey(selectedIndex.X) &&
                        m_items[selectedIndex.X].TryGetValue(selectedIndex.Y, out item))
                    {
                        if (m_selectedItem != null)
                        {
                            m_selectedItem.Selected = false;
                        }
                        m_selectedIndex = selectedIndex;
                        m_selectedItem = item;
                        m_selectedItem.Selected = true;

                        if (SelectedItemChanged != null)
                        {
                            SelectedItemChanged(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    if (SelectedItemClicked != null)
                    {
                        SelectedItemClicked(this, new EventArgs());
                    }
                }

                m_velocity.X = 0;
                m_velocity.Y = 0;
            }
            else
            {
                m_timer.Enabled = true;
            }

            m_mouseDown.Y = -1;
            Capture = false;

            Invalidate();
        }

        /// <summary>
        /// Resets the drawing of the list.
        /// </summary>
        private void Reset()
        {
            if (!m_updating)
            {
                m_timer.Enabled = false;
                if (m_selectedItem != null)
                {
                    m_selectedItem.Selected = false;
                    m_selectedItem = null;
                }
                m_selectedIndex = new Point(-1, -1);
                Capture = false;
                m_velocity.X = 0;
                m_velocity.Y = 0;
                m_offset.X = 0;
                m_offset.Y = 0;

                Invalidate();

                if (SelectedItemChanged != null)
                {
                    SelectedItemChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Cleans up the background paint buffer.
        /// </summary>
        private void CleanupBackBuffer()
        {
            if (m_backBufferBitmap != null)
            {
                m_backBufferBitmap.Dispose();
                m_backBufferBitmap = null;
                m_backBuffer.Dispose();
                m_backBuffer = null;
            }
        }

        /// <summary>
        /// Creates the background paint buffer.
        /// </summary>
        private void CreateBackBuffer()
        {
            CleanupBackBuffer();

            m_backBufferBitmap = new Bitmap(Bounds.Width, Bounds.Height);
            m_backBuffer = Graphics.FromImage(m_backBufferBitmap);
        }

        /// <summary>
        /// Clips the scroll position.
        /// </summary>
        private void ClipScrollPosition()
        {
            if (m_offset.X < 0)
            {
                m_offset.X = 0;
                m_velocity.X = 0;
            }
            else if (m_offset.X > MaxXOffset)
            {
                m_offset.X = MaxXOffset;
                m_velocity.X = 0;
            }
            if (m_offset.Y < 0)
            {
                m_offset.Y = 0;
                m_velocity.Y = 0;
            }
            else if (m_offset.Y > MaxYOffset)
            {
                m_offset.Y = MaxYOffset;
                m_velocity.Y = 0;
            }
        }

        /// <summary>
        /// Clips the velocity.
        /// </summary>
        private void ClipVelocity()
        {
            m_velocity.X = Math.Min(m_velocity.X, m_maxVelocity);
            m_velocity.X = Math.Max(m_velocity.X, -m_maxVelocity);

            m_velocity.Y = Math.Min(m_velocity.Y, m_maxVelocity);
            m_velocity.Y = Math.Max(m_velocity.Y, -m_maxVelocity);
        }

        /// <summary>
        /// Finds the bounds for the specified item.
        /// </summary>
        /// <param name="x">The item x index.</param>
        /// <param name="y">The item y index.</param>
        /// <returns>The item bounds.</returns>
        private Rectangle ItemBounds(int x, int y)
        {
            int itemX = Bounds.Left + (m_itemWidth * x);
            int itemY = Bounds.Top + (m_itemHeight * y);

            if (m_layout == KListLayout.Vertical)
            {
                return new Rectangle(Bounds.Left, itemY, ItemWidth, ItemHeight);
            }
            else if (m_layout == KListLayout.Horizontal)
            {
                return new Rectangle(itemX, Bounds.Top, ItemWidth, ItemHeight);
            }
            else
            {
                return new Rectangle(itemX, itemY, ItemWidth, ItemHeight);
            }
        }

        /// <summary>
        /// Finds the index for the specified y offset.
        /// </summary>
        /// <param name="x">The x offset.</param>
        /// <param name="y">The y offset.</param>
        /// <returns></returns>
        private Point FindIndex(int x, int y)
        {
            Point index = new Point(0, 0);

            if (m_layout == KListLayout.Vertical)
            {
                index.Y = ((y + m_offset.Y - Bounds.Top) / (m_itemHeight));
            }
            else if (m_layout == KListLayout.Horizontal)
            {
                index.X = ((x + m_offset.X - Bounds.Left) / (m_itemWidth));
            }
            else
            {
                index.X = ((x + m_offset.X - Bounds.Left) / (m_itemWidth));
                index.Y = ((y + m_offset.Y - Bounds.Top) / (m_itemHeight));
            }

            return index;
        }

        /// <summary>
        /// Gets the maximum x offset.
        /// </summary>
        /// <value>The maximum x offset.</value>
        private int MaxXOffset
        {
            get
            {
                return Math.Max(((m_items.Count * ItemWidth)) - Bounds.Width, 0);
            }
        }

        /// <summary>
        /// Gets the maximum y offset.
        /// </summary>
        /// <value>The maximum y offset.</value>
        private int MaxYOffset
        {
            get
            {
                if (m_items.Count > 0)
                {
                    return Math.Max(((m_items[0].Count * ItemHeight)) - Bounds.Height, 0);
                }
                else
                {
                    return 0;
                }
            }
        }

        // The items!
        class ItemList : Dictionary<int, IKListItem>
        {
        }
        class GridList : Dictionary<int, ItemList>
        {
        }
        GridList m_items = new GridList();

        /// <summary>
        /// Layout Mode.
        /// </summary>
        public enum KListLayout
        {
            /// <summary>
            /// Vertically scrolling list.
            /// </summary>
            Vertical,

            /// <summary>
            /// Horizontally scrolling list.
            /// </summary>
            Horizontal,

            /// <summary>
            /// Scrolling grid.
            /// </summary>
            Grid
        };

        // Properties
        int m_maxVelocity = 15;
        int m_itemHeight = 38;
        int m_itemWidth = 80;
        KListLayout m_layout = KListLayout.Vertical;
        bool m_updating = false;

        // Background drawing
        Bitmap m_backBufferBitmap;
        Graphics m_backBuffer;

        // Motion variables
        Point m_selectedIndex = new Point(-1, -1);
        IKListItem m_selectedItem = null;
        Point m_velocity = new Point(0, 0);
        Point m_mouseDown = new Point(-1, -1);
        Point m_mousePrev = new Point(-1, -1);
        Timer m_timer = new Timer();
        int m_timerCount = 0;
        Point m_offset = new Point();


        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return m_items[0].Values.GetEnumerator();
        }

        #endregion
    }
}
