using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tenor.Mobile.Diagnostics;
using Tenor.Mobile.UI;
using AutoRotationConfig.Properties;

namespace AutoRotationConfig
{
    public partial class ControlPanel : Form
    {
        internal static bool haveChanged = false;

        public ControlPanel()
        {
            InitializeComponent();

            Tenor.Mobile.UI.Skin.Current.ApplyColorsToControl(this);
            Tenor.Mobile.UI.Skin.Current.ApplyColorsToControl(ucHelp);
            Tenor.Mobile.UI.Skin.Current.ApplyColorsToControl(ucApplications);

            ucApplications.SetMenus(mnuAdd, mnuRemove);

            titleStrip.Tabs.Add(new HeaderTab("Applications", Resources.app_selected));
            titleStrip.Tabs.Add(new HeaderTab("About", Resources.help));


        }


        RotationConfig Config
        {
            get
            {
                return Program.Config;
            }
        }


        private void titleStrip_SelectedTabChanged(object sender, EventArgs e)
        {
            switch (titleStrip.SelectedIndex)
            {
                case 0:
                    titleStrip.Tabs[0].Image = Resources.app_selected;
                    titleStrip.Tabs[1].Image = Resources.help;


                    ucHelp.Visible = false;

                    ucApplications.Dock = DockStyle.Fill;
                    ucApplications.BringToFront();
                    ucApplications.Visible = true;

                    ucApplications.LoadConfiguredApps();
                    ucApplications.LoadRunningApps();

                    break;
                case 1:
                    titleStrip.Tabs[0].Image = Resources.app;
                    titleStrip.Tabs[1].Image = Resources.help_selected;

                    ucApplications.Visible = false;
                    ucHelp.Dock = DockStyle.Fill;
                    ucHelp.BringToFront();
                    ucHelp.Visible = true;

                    mnuAdd.Enabled = false;
                    mnuRemove.Enabled = false;
                    break;

            }
        }








        private void ControlPanel_Closing(object sender, CancelEventArgs e)
        {
            if (ControlPanel.haveChanged)
            {
                bool message = false;
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();
                Config.Enabled = true;
                message = !Config.ReloadRotationSupport();
                Cursor.Current = Cursors.Default;
                Cursor.Show();
                if (message)
                {
                    if (MessageBox.Show("Your changes may be applied after a soft-reset. If not, your phone's built-in g-sensor is disabled.\r\n\r\nDo you want to soft-reset now?", "Auto-Rotate", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) 
                        == DialogResult.Yes)
                    {
                        Config.RestartSystem();
                    }
                }
            }
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            titleStrip.SelectedIndex = 0;
            titleStrip_SelectedTabChanged(titleStrip, new EventArgs());
        }

        private void ControlPanel_Activated(object sender, EventArgs e)
        {
            ucApplications.LoadRunningApps();
        }


    }
}