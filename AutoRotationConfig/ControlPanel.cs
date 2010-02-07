using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tenor.Mobile.Diagnostics;

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
            int height = Convert.ToInt32(1 * this.scaleFactor.Height);
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

            //int oldIndex = (appList.SelectedItem == null ? 0 : appList.SelectedItem.YIndex);
            appList.Clear();
            appList.DrawSeparators = true;
            foreach (AppDetails app in config.Applications)
            {
                appList.AddItem(app.Title, app);
            }
        }

        #region Enumerate Windows
        private void ControlPanel_Activated(object sender, EventArgs e)
        {
            LoadRunningApps();
        }
        List<string> windows = new List<string>();
        List<Process> runningProc = new List<Process>();

        private void LoadRunningApps()
        {
            runningProc.Clear();
            windows.Clear();
            //adding exceptions:
            windows.Add("MS_SIPBUTTON");
            foreach(AppDetails app in config.Applications)
                windows.Add(app.Title);

            mnuAdd.MenuItems.Clear();
            mnuAdd.Enabled = false;

            Process[] allProcs = Process.GetProcesses();
            foreach (Tenor.Mobile.Diagnostics.Window w in Window.GetWindows())
            {
                if (w.Visible && !string.IsNullOrEmpty(w.Text) && !windows.Contains(w.Text))
                {
                    MenuItem m = new MenuItem();
                    m.Text = w.Text.Replace("&", "&&");
                    m.Click += new EventHandler(WindowMenu_Click);
                    mnuAdd.MenuItems.Add(m);
                    windows.Add(w.Text);
                    mnuAdd.Enabled = true;

                    bool added = false;
                    foreach (Process p in allProcs)
                    {
                        if (p.Id == w.ProcessId)
                        {
                            runningProc.Add(p); added = true;
                            break;
                        }
                    }
                    if (!added) runningProc.Add(null);
                }
            }
        }



        #endregion

        private void WindowMenu_Click(object sender, EventArgs e)
        {
            MenuItem menu = ((MenuItem)sender);
            string title = menu.Text.Replace("&&", "&");
            RunningApp app = new RunningApp() { Title = title };
            app.Process = runningProc[menu.Parent.MenuItems.IndexOf(menu)];
            config.AddApplication(app);

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
                MessageBox.Show("Your changes may be applied after a soft-reset. If not, your phone's built-in g-sensor is disabled.", "Auto-Rotate");

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            config = new RotationConfig();
            LoadConfiguredApps();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private SizeF scaleFactor;
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            this.scaleFactor = factor;
            base.ScaleControl(factor, specified);
        }

        private void appList_DrawItem(object sender, Tenor.Mobile.UI.DrawItemEventArgs e)
        {
            AppDetails app = (AppDetails)e.Item.Value;

            Graphics g = e.Graphics;

            

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            SolidBrush textBrush;
            if (e.Item.Selected)
            {
                SolidBrush backBrush;
                backBrush = new SolidBrush(SystemColors.Highlight);
                textBrush = new SolidBrush(SystemColors.HighlightText);
                g.FillRectangle(backBrush, e.Bounds);
            }
            else
            {
                //backBrush = new SolidBrush(SystemColors.Window);
                textBrush = new SolidBrush(SystemColors.ControlText);
            }
            int iconWidth = Convert.ToInt32(39 * scaleFactor.Width);
            Rectangle rect = e.Bounds;
            rect.X += iconWidth;
            rect.Width -= iconWidth;


            g.DrawString(e.Item.Text, e.Item.Parent.Font, textBrush, rect, format);
            int offsetX = Convert.ToInt32(3 * scaleFactor.Width);
            int offsetY = Convert.ToInt32(3 * scaleFactor.Height);

            List<string> fileNames = new List<string>(app.PossibleLocations.ToArray());
            fileNames.Add("\\Windows\\shell32.exe");
            foreach (string fileName in fileNames)
            {
                if (System.IO.File.Exists(fileName))
                {
                    using (Icon icon = Tenor.Mobile.Drawing.IconHelper.ExtractAssociatedIcon(fileName, true))
                    {
                        g.DrawIcon(icon, e.Bounds.X + offsetX, e.Bounds.Y + offsetY);
                    }
                    break;
                }
            }

        }



    }
}