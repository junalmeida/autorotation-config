using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoRotationConfig
{
    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int height = Convert.ToInt32(1 * this.factor.Height);
            Rectangle rect = new Rectangle(e.ClipRectangle.Left, panel1.Bottom - height, e.ClipRectangle.Width, height);
            if (e.ClipRectangle.IntersectsWith(rect))
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlText), rect);
            }
        }


        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == 0)
            {
                LoadConfiguredApps();
                LoadRunningApps();
            }
            else
            {
                mnuAdd.Enabled = false;
                mnuRemove.Enabled = false;
            }
        }

        RotationConfig config;


        private void LoadConfiguredApps()
        {
            mnuRemove.Enabled = false;
            //list.Checked = config.Enabled;

            int oldIndex = (appList.SelectedItem == null ? 0 : appList.SelectedItem.YIndex);
            appList.Clear();
            foreach (string app in config.Applications)
            {
                appList.AddItem(app, app);
            }
        }

        #region Enumerate Windows
        private void ControlPanel_Activated(object sender, EventArgs e)
        {
            LoadRunningApps();
        }
        List<string> windows = new List<string>();
        private void LoadRunningApps()
        {
            windows.Clear();
            //adding exceptions:
            windows.Add("MS_SIPBUTTON");
            windows.AddRange(config.Applications);

            mnuAdd.MenuItems.Clear();
            mnuAdd.Enabled = false;
            ProcessEnumerator.ListWindows(new ProcessEnumerator.EnumWindowsProc(CreateMenuItem));
        }

        private int CreateMenuItem(IntPtr handle, IntPtr param)
        {
            if (ProcessEnumerator.IsWindowVisible(handle))
            {
                string title = ProcessEnumerator.GetWindowText(handle);
                string className = ProcessEnumerator.GetWindowClass(handle);
                if (!string.IsNullOrEmpty(title) && !windows.Contains(title))
                {
                    MenuItem m = new MenuItem();
                    m.Text = title.Replace("&", "&&");
                    m.Click += new EventHandler(WindowMenu_Click);
                    mnuAdd.MenuItems.Add(m);
                    windows.Add(title);
                    mnuAdd.Enabled = true;
                }
            }
            return 1;
        }


        #endregion

        private void WindowMenu_Click(object sender, EventArgs e)
        {
            string title = ((MenuItem)sender).Text.Replace("&&", "&");
            config.AddApplication(title);

            LoadConfiguredApps();
            LoadRunningApps();
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            if (appList.SelectedItem != null)
            {
                config.RemoveApplication(appList.SelectedItem.YIndex);
                LoadConfiguredApps();
                LoadRunningApps();
            }
        }

        private void appList_SelectedItemChanged(object sender, EventArgs e)
        {
            mnuRemove.Enabled = appList.SelectedItem != null;

        }

        private void ControlPanel_Closing(object sender, CancelEventArgs e)
        {
            bool message = false;
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();
            message = !config.ReloadRotationSupport();
            Cursor.Current = Cursors.Default;
            Cursor.Show();
            if (message)
                MessageBox.Show("Your changes will be applied after a soft-reset.", "Auto-Rotate");

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            config = new RotationConfig();
            config.CheckDevice();
            LoadConfiguredApps();

            AdjustTabControl();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        bool styleDone = false;
        private void AdjustTabControl()
        {
            try
            {
                int baseHeight = 20;
                int newHeight = Convert.ToInt32(baseHeight * factor.Height);

                IntPtr window = NativeMethods.GetWindow(tabs.Handle, NativeMethods.GW.CHILD);
                int num = NativeMethods.SendMessage(window, NativeMethods.WMSG.TCM_SETITEMSIZE, 0, NativeMethods.MakeLParam(new Point(0, newHeight)));

                int offset = 11;
                if (factor.Height > 1)
                    foreach (TabPage t in tabs.TabPages)
                    {
                        t.Text = string.Format("  {0}  ", t.Text);
                        NativeMethods.SetWindowPos(t.Handle, IntPtr.Zero, 0, 0, t.Width, t.Height - (newHeight - baseHeight) + offset, NativeMethods.SWP.SWP_NOACTIVATE | NativeMethods.SWP.SWP_NOMOVE | NativeMethods.SWP.SWP_NOREPOSITION | NativeMethods.SWP.SWP_NOZORDER);
                    }

                if (!styleDone)
                {
                    num = NativeMethods.GetWindowLong(window, NativeMethods.GWL.STYLE).ToInt32();
                    num = NativeMethods.SetWindowLong(window, NativeMethods.GWL.STYLE, (int)(num | 0x4000));
                    styleDone = true;
                }

            }
            catch (Win32Exception)
            { }

        }

        private SizeF factor;

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            this.factor = factor;
            base.ScaleControl(factor, specified);

            appList.ItemHeight = Convert.ToInt32(appList.ItemHeight * factor.Height);
        }



    }
}