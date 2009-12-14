using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoRotationConfig
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadApps();
            ReloadRunningApps();
        }
        RotationConfig config = new RotationConfig();


        private void LoadApps()
        {
            listBox.Items.Clear();

            foreach (string app in config.Applications)
            {
                    listBox.Items.Add(app);
            }
            mnuRemove.Enabled = false;
            chkEnable.Checked = config.Enabled;
       }


        private void ReloadRunningApps()
        {
            windows.Clear();
            //adding exceptions:
            windows.Add("CursorWindow");
            windows.Add("MS_SIPBUTTON");

            foreach (string title in listBox.Items)
                windows.Add(title);
            
            mnuAdd.MenuItems.Clear();
            mnuAdd.Enabled = false;
            ProcessEnumerator.ListWindows(new ProcessEnumerator.EnumWindowsProc(CreateMenuItem));
        }

        List<string> windows = new List<string>();

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
                    m.Click += new EventHandler(m_Click);
                    mnuAdd.MenuItems.Add(m);
                    windows.Add(title);
                    mnuAdd.Enabled = true;
                }
            }
            return 1;
        }

        void m_Click(object sender, EventArgs e)
        {
            string title = ((MenuItem)sender).Text.Replace("&&", "&");
            config.AddApplication(title);
            listBox.Items.Add(title);

            ReloadRunningApps();
        }


        private void mnuRemove_Click(object sender, EventArgs e)
        {
            config.RemoveApplication(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);

            ReloadRunningApps();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mnuRemove.Enabled = listBox.SelectedIndex >= 0;
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            ReloadRunningApps();
        }

        private void Main_GotFocus(object sender, EventArgs e)
        {

        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            config.ReloadRotationSupport();
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            config.ReloadRotationSupport();
        }

        private void chkEnable_CheckStateChanged(object sender, EventArgs e)
        {
            config.Enabled = chkEnable.Checked;
        }

    }
}