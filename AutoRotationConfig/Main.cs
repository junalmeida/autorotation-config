using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Fluid.Controls;

namespace AutoRotationConfig
{
    public partial class Main : Form
    {

        RotationConfig config;
        public Main()
        {
            InitializeComponent();

            config = new RotationConfig();

            fluidHost.Bounds = fluidHost.ClientBounds;
            fluidHost.BackColor = Color.Empty;

            if (config.FirstTime)
            {
                WelcomeScreen welcome = new WelcomeScreen();
                welcome.Continue += new EventHandler(welcome_Continue);
                welcome.ShowMaximized(ShowTransition.None);

                config.FirstTime = false;
            }
            else
            {
                LoadList();
            }

        }

        void welcome_Continue(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            list = new ApplicationList();
            list.ShowMaximized(ShowTransition.FromBottom);
            list.SelectedIndexChanged += new EventHandler(list_SelectedIndexChanged);
            list.CheckedChanged += new EventHandler(list_CheckedChanged);
            LoadApps();
            ReloadRunningApps();
        }

        void list_CheckedChanged(object sender, EventArgs e)
        {
            config.Enabled = list.Checked;
        }

        ApplicationList list;





        private void LoadApps()
        {
            mnuRemove.Enabled = false;
            //list.Checked = config.Enabled;
            int oldIndex = list.SelectedIndex;
            list.DataSource = config.Applications;
            if (oldIndex > list.Count - 1)
                oldIndex = list.Count - 1;
            list.SelectedIndex = oldIndex;
            mnuRemove.Enabled = list.SelectedIndex >= 0;

            list.Checked = config.Enabled;
       }


        private void ReloadRunningApps()
        {
            windows.Clear();
            //adding exceptions:
            windows.Add("MS_SIPBUTTON");
            windows.AddRange(config.Applications);
           
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

            LoadApps();
            ReloadRunningApps();
        }


        private void mnuRemove_Click(object sender, EventArgs e)
        {
            config.RemoveApplication(list.SelectedIndex);
            LoadApps();
            ReloadRunningApps();
        }

        private void list_SelectedIndexChanged(object sender, EventArgs e)
        {
            mnuRemove.Enabled = list.SelectedIndex >= 0;
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

        //private void chkEnable_CheckStateChanged(object sender, EventArgs e)
        //{
        //    config.Enabled = chkEnable.Checked;
        //}

    }
}