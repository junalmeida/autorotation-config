using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
namespace AutoRotationConfig
{
    /// <summary>
    /// A basic implementation of the IKListItem interface.
    /// </summary>
    public class AppListItem : KListControl.IKListItem, IDisposable
    {
        /// <summary>
        /// Initializes the <see cref="KListItem"/> class.
        /// </summary>
        static AppListItem()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            m_parent = null;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="KListItem"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get { return m_selected;  } set { m_selected = value; } }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get { return m_text; } set { m_text = value; } }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get { return m_value; } set { m_value = value; } }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public KListControl Parent { get { return m_parent; } set { m_parent = value; } }

        /// <summary>
        /// The unscrolled bounds for this item.
        /// </summary>
        public Rectangle Bounds { get { return m_bounds; } set { m_bounds = value; } }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public int XIndex { get { return m_x; } set { m_x = value; } }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public int YIndex { get { return m_y; } set { m_y = value; } }


        /// <summary>
        /// Renders to the specified graphics.
        /// </summary>
        /// <param name="g">The graphics.</param>
        /// <param name="bounds">The bounds.</param>
        public virtual void Render(Graphics g, Rectangle bounds)
        {
            PaintListItem(g, bounds);
            DrawAppName(g, bounds);

            //FreeStore.Forms.ApplicationList appList = this.Parent.Parent as FreeStore.Forms.ApplicationList;
            //if (appList != null && Application != null)
            //{
            //    switch (appList.ContentType)
            //    {
            //        case ContentType.Description:
            //            DrawDescription(g, bounds);
            //            break;
            //        case ContentType.InstalledApplication:
            //            DrawInstalledApp(g, bounds);
            //            break;
            //        case ContentType.Download:
            //            DrawDownloadStatus(g, bounds);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //else if (Category != null)
            //{
            //    DrawCategory(g, bounds);
            //}
        }


        private void DrawAppName(Graphics g, Rectangle bounds)
        {
            string appName = this.Text;

            Brush brush;



            if (Selected)
                brush = new SolidBrush(SystemColors.HighlightText);
            else
                brush = new SolidBrush(SystemColors.ControlText);

            g.DrawString(appName, Parent.Font, brush, bounds.X + 18, bounds.Y + 3);
        }


        

        private void PaintListItem(Graphics graphics, Rectangle bounds)
        {
            Brush brush;
            if (Selected)
                brush = new SolidBrush(SystemColors.Highlight);
            else
                brush = new SolidBrush(SystemColors.Window);

            graphics.FillRectangle(brush, bounds);

            if (!Selected)
            {
                Color penColor = Color.FromArgb(0xB5, 0xB6, 0xB5);

                Pen pen = new Pen(SystemColors.InactiveBorder, 1);
                graphics.DrawLine(pen,
                    bounds.X, bounds.Bottom - 1,
                    bounds.Right, bounds.Bottom - 1
                    );
            }
        }

        private StringFormat m_stringFormat = new StringFormat();
        private KListControl m_parent;
        private Rectangle m_bounds;
        private int m_x = -1;
        private int m_y = -1;

        private string m_text;
        private object m_value;
        private bool m_selected = false;

    }
}
