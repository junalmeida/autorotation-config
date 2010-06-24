using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Tenor.Mobile.Diagnostics;

namespace AutoRotationConfig
{
    public partial class ApplicationList : UserControl
    {

        private SizeF scaleFactor;
        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            this.scaleFactor = factor;
            base.ScaleControl(factor, specified);
        }

        RotationConfig Config
        {
            get
            {
                return Program.Config;
            }
        }

        public ApplicationList()
        {
            InitializeComponent();
        }

        MenuItem mnuRemove;
        MenuItem mnuAdd;

        internal void SetMenus(MenuItem left, MenuItem right)
        {
            mnuAdd = left;
            mnuRemove = right;

            mnuRemove.Click += new EventHandler(mnuRemove_Click);
        }

        void mnuRemove_Click(object sender, EventArgs e)
        {
            Remover();
        }






        StringFormat format = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };
        Font f = new Font(FontFamily.GenericSansSerif, 12F, FontStyle.Regular);
        private void appList_DrawItem(object sender, Tenor.Mobile.UI.DrawItemEventArgs e)
        {
            AppDetails app = (AppDetails)e.Item.Value;

            Graphics g = e.Graphics;



            SolidBrush textBrush;
            if (e.Item.Selected)
            {
                //SolidBrush backBrush;
                //backBrush = new SolidBrush(SystemColors.Highlight);
                //g.FillRectangle(backBrush, e.Bounds);
                textBrush = new SolidBrush(Tenor.Mobile.UI.Skin.Current.TextHighLight);
            }
            else
            {
                //backBrush = new SolidBrush(SystemColors.Window);
                textBrush = new SolidBrush(Tenor.Mobile.UI.Skin.Current.TextForeColor);
            }
            int iconWidth = Convert.ToInt32(39 * scaleFactor.Width);
            Rectangle rect = e.Bounds;
            rect.X += iconWidth + Convert.ToInt32(4 * scaleFactor.Width);
            rect.Width -= iconWidth - Convert.ToInt32(4 * scaleFactor.Width);


            g.DrawString(e.Item.Text, f, textBrush, rect, format);

            DrawIcon(app, e);

            textBrush.Dispose();
        }




        private Dictionary<AppDetails, Icon> iconCache = new Dictionary<AppDetails, Icon>();
        private void DrawIcon(AppDetails app, Tenor.Mobile.UI.DrawItemEventArgs e)
        {
            int offsetX = Convert.ToInt32(3 * scaleFactor.Width);
            int offsetY = Convert.ToInt32(3 * scaleFactor.Height);

            Icon icon = null;
            if (iconCache.ContainsKey(app))
                icon = iconCache[app];
            else
            {
                List<string> fileNames = new List<string>();


                fileNames.AddRange(Config.GetPossibleLocations(app.Title));
                fileNames.AddRange(app.PossibleLocations);

                foreach (string fileName in fileNames)
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        icon = Tenor.Mobile.Drawing.IconHelper.ExtractAssociatedIcon(fileName, true);
                        iconCache.Add(app, icon);
                        break;
                    }
                }
            }
            if (icon != null)
                e.Graphics.DrawIcon(icon, e.Bounds.X + offsetX, e.Bounds.Y + offsetY);
        }


        public void LoadConfiguredApps()
        {
            mnuRemove.Enabled = false;
            //list.Checked = config.Enabled;

            //int oldIndex = (appList.SelectedItem == null ? 0 : appList.SelectedItem.YIndex);
            appList.Clear();
            appList.DrawSeparators = true;
            foreach (AppDetails app in Config.Applications)
            {
                appList.AddItem(app.Title, app);
            }
        }

        #region Enumerate Windows
        List<string> windows = new List<string>();
        List<RunningApp> runningWindows = new List<RunningApp>();

        internal void LoadRunningApps()
        {

            windows.Clear();
            //adding exceptions:
            windows.Add("MS_SIPBUTTON");
            windows.Add("CursorWindow");
            foreach (AppDetails app in Config.Applications)
                windows.Add(app.Title);

            runningWindows.Clear();
            mnuAdd.MenuItems.Clear();
            mnuAdd.Enabled = false;

            Process[] allProcs = Process.GetProcesses();

            List<Window> allWindows = new List<Window>(Window.GetWindows());
            allWindows.Sort(new Comparison<Window>(delegate(Window a, Window b)
            {
                return string.Compare(a.Text, b.Text);
            }));

            foreach (Tenor.Mobile.Diagnostics.Window w in allWindows)
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
                            added = true;
                            runningWindows.Add(new RunningApp()
                            {
                                Process = p,
                                Title = w.Text,
                                ClassName = w.ClassName

                            });
                            break;
                        }
                    }
                    if (!added) runningWindows.Add(null);
                }
            }
        }

        #endregion

        private void WindowMenu_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            MenuItem menu = ((MenuItem)sender);
            RunningApp app = runningWindows[menu.Parent.MenuItems.IndexOf(menu)];
            Config.AddApplication(app);

            LoadConfiguredApps();
            LoadRunningApps();

            Cursor.Current = Cursors.Default;
            Cursor.Show();
        }

        private void Remover()
        {
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Show();

            if (appList.SelectedItem != null)
            {
                Config.RemoveApplication(appList.SelectedItem.YIndex);
                LoadConfiguredApps();
                LoadRunningApps();
            }


            Cursor.Current = Cursors.Default;
            Cursor.Show();
        }

        private void appList_SelectedItemChanged(object sender, EventArgs e)
        {
            mnuRemove.Enabled = appList.SelectedItem != null;

        }
    }
}
