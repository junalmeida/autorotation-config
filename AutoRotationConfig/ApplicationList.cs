using System;

using System.Collections.Generic;
using System.Text;
using Fluid.Controls;
using System.Drawing;

namespace AutoRotationConfig
{
    public class ApplicationList : FluidPanel
    {
        Header header;
        FluidListBox list;
        FluidButton enabled;
        ItemTemplate template;
        protected override void InitControl()
        {
            base.InitControl();



            Bounds = new Rectangle(0, 0, 240, 300);

            BackColor = Color.Black;

            header = new Header();
            header.Text = "Whitelist";
            Controls.Add(header);

            template = new ItemTemplate();
            list = new FluidListBox(0, header.Height, Bounds.Width, Bounds.Height - header.Height);

            list.BackColor = Color.White;
            list.ForeColor = Color.Black;
            list.BorderColor = Color.Silver;
            list.ScrollBarButtonColor = Color.Black;
            list.ScrollBarButtonBorderColor = Color.Silver;

            list.SelectedIndexChanged += new EventHandler<ChangedEventArgs<int>>(list_SelectedIndexChanged);

            list.ItemHeight = 30;
            list.Anchor = AnchorAll;
            
            Controls.Add(list);

            enabled = new FluidButton(string.Empty, Bounds.Width - 30, 4, 27, 27);
            enabled.Shape = ButtonShape.Rectangle;
            enabled.Click += new EventHandler(enabled_Click);
            enabled.BackColor = Color.FromArgb(131, 135, 136);
            enabled.ForeColor = Color.White;
            enabled.Anchor = AnchorTR;
            Checked = false;
            Controls.Add(enabled);

        }

        public event EventHandler CheckedChanged;
        public event EventHandler SelectedIndexChanged;

        void list_SelectedIndexChanged(object sender, ChangedEventArgs<int> e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new EventArgs());
        }

        bool check;

        public bool Checked
        {
            get { return check; }
            set
            {
                check = value;
                if (check)
                    enabled.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("AutoRotationConfig.close_toolbar_16x16.gif"));
                else
                    enabled.Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("AutoRotationConfig.16-em-check_16x16.png"));
            }
        }


        void enabled_Click(object sender, EventArgs e)
        {
            Checked = !Checked;
            if (CheckedChanged != null)
                CheckedChanged(this, new EventArgs());

        }

        public int SelectedIndex
        {
            get { return list.SelectedItemIndex; }
            set { list.SelectedItemIndex = value; }
        }

        public int Count
        {
            get { return list.ItemCount; }
        }
            
        

        public string[] DataSource
        {
            get { return (string[])list.DataSource; }
            set
            {
                list.DataSource = value;
                list.Bind(template);
            }
        }


        private class ItemTemplate : FluidTemplate
        {

            public ItemTemplate()
                : base()
            {
            }


            private FluidLabel label;
            private FluidButton deleteBtn;
            private FluidButton confirmDeleteBtn;


            protected override void InitControl()
            {
                base.InitControl();
                //ButtonStretch = 100;
                BackColor = Color.Black;

                Font buttonFont = new Font(FontFamily.GenericSansSerif, 8f, FontStyle.Bold);

                this.Bounds = new Rectangle(0, 0, 240, 50);
                FluidLabel l = new FluidLabel("", 8, 0, 224, 50);
                l.LineAlignment = StringAlignment.Center;
                l.Anchor = AnchorAll;
                label = l;
                l.Font = new Font(FontFamily.GenericSansSerif, 11f, FontStyle.Bold);

                Controls.Add(l);

                deleteBtn = new FluidButton("-", 4, 7, 20, 20);
                deleteBtn.ButtonSize = new Size(20, 20);
                deleteBtn.Shape = ButtonShape.Ellipse;
                deleteBtn.BackColor = Color.DarkRed; // Salmo, IndianRed Tomato
                deleteBtn.ForeColor = Color.White;
                deleteBtn.Font = buttonFont;
                //deleteBtn.Click += new EventHandler(deleteBtn_Click);
                deleteBtn.Anchor = AnchorTL;
                deleteBtn.Visible = false;
                Controls.Add(deleteBtn);

                confirmDeleteBtn = new FluidButton("Delete", 240 - 50 - 4, 4, 50, 50 - 8);
                confirmDeleteBtn.ButtonSize = new Size(50, 24);
                confirmDeleteBtn.BackColor = Color.Red;
                confirmDeleteBtn.ForeColor = Color.White;
                confirmDeleteBtn.Font = buttonFont;
                confirmDeleteBtn.Anchor = AnchorRTB;
                //confirmDeleteBtn.Click += new EventHandler(confirmDeleteBtn_Click);
                confirmDeleteBtn.Visible = false;
                Controls.Add(confirmDeleteBtn);


            }
            /*

            //CategoriesListBox ListBox { get { return Parent as CategoriesListBox; } }

            int deleteIndex = -1;

            public int DeleteIndex
            {
                get { return deleteIndex; }
                set
                {
                    if (deleteIndex != value)
                    {
                        if (deleteIndex >= 0) ListBox.Invalidate(ListBox.GetItemBounds(deleteIndex));
                        deleteIndex = value;
                        if (value >= 0) ListBox.Invalidate(ListBox.GetItemBounds(deleteIndex));
                        ListBox.Invalidate();
                    }
                }
            }

            void confirmDeleteBtn_Click(object sender, EventArgs e)
            {
                int deleteIndex = DeleteIndex;
                DeleteIndex = -1;
                ListBox.Categories.RemoveAt(deleteIndex);
            }

            public bool ShowCounter { get; set; }


            void deleteBtn_Click(object sender, EventArgs e)
            {
                int index = this.ItemIndex;
                ListBox.SelectedItemIndex = index;
                DeleteIndex = index;
            }

            protected override void OnBindValue(object value)
            {
                PasswordCategory c = value as PasswordCategory;
                if (c != null)
                {
                    LayoutTemplate(c);
                    label.Text = c.Title;

                    if (ShowCounter)
                    {
                        label.ForeColor = c.PasswordCount == 0 ? Theme.Current.GrayTextColor : Color.Empty;
                        countLabel.Text = c.PasswordCount.ToString();
                        confirmDeleteBtn.Visible = deleteIndex >= 0 && ItemIndex == deleteIndex;
                    }
                }
                else
                {
                    label.Text = "";
                }
            }

            public CategoriesListBox CategoryListBox { get { return Parent as CategoriesListBox; } }

            protected override void OnItemUpdate(object value)
            {
                base.OnItemUpdate(value);
                if (ShowCounter)
                {
                    bool isSelected = this.CategoryListBox.SelectedItemIndex == this.ItemIndex;
                    countLabel.ForeColor = isSelected ? Theme.Current.ListSecondarySelectedForeColor : Theme.Current.ListSecondaryForeColor;
                }
            }

            private void LayoutTemplate(PasswordCategory c)
            {
                if (c != null) deleteBtn.Visible = editMode && !c.ReadOnly;
            }

            private bool editMode;
            public bool EditMode
            {
                get { return editMode; }
                set
                {
                    if (editMode != value)
                    {
                        editMode = value;
                        OnEditModeChanged();
                    }
                }
            }

            private int buttonStretch = 100;
            public int ButtonStretch
            {
                get { return buttonStretch; }
                set
                {
                    if (buttonStretch != value)
                    {
                        buttonStretch = value;
                        LayoutTemplate();
                    }
                }
            }


            private void OnEditModeChanged()
            {
                if (!editMode) DeleteIndex = -1;
                LayoutTemplate();
            }

            private void LayoutTemplate()
            {
                BeginInit();
                deleteBtn.Visible = editMode;
                int w = Width;
                int h = Height;
                int distance = ScaleX(8);
                if (editMode) deleteBtn.Width = buttonStretch * ScaleX(20) / 100;
                int l = (editMode ? deleteBtn.Width : 0) + distance;
                label.Bounds = new Rectangle(l, 0, w - l, h);
                EndInit();
            }
            */

        }

    }
}
