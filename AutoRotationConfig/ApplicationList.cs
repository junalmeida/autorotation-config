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

        internal MenuItem mnuRemove;
        internal MenuItem mnuAdd;




        Font f = new Font(FontFamily.GenericSansSerif, 12F, FontStyle.Regular);
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
            int offsetX = Convert.ToInt32(3 * scaleFactor.Width);
            int offsetY = Convert.ToInt32(3 * scaleFactor.Height);

            List<string> fileNames = new List<string>(app.PossibleLocations.ToArray());
            if (app.Title == "Notes")
                fileNames.Add("\\Windows\\notes.exe");
            else if (app.Title == "Word Mobile")
                fileNames.Add("\\Windows\\pword.exe");
            else if (app.Title == "Excel Mobile")
                fileNames.Add("\\Windows\\pxl.exe");
            else if (app.Title == "PowerPoint Mobile")
                fileNames.Add("\\Windows\\ppt.exe");
            else if (app.Title == "OneNote Mobile")
                fileNames.Add("\\Windows\\OneNoteMobile.exe");
            else if (app.Title == "Outlook E-mail")
                fileNames.Add("\\Windows\\tmail.exe");
            else if (app.Title == "Messaging")
                fileNames.Add("\\Windows\\tmail.exe");
            else if (app.Title == "Pictures & Videos")
                fileNames.Insert(0, "\\Windows\\Start Menu\\Programs\\Pictures & Videos.lnk");
            else if (app.Title == "Desktop")
                fileNames.Insert(0, "\\Windows\\fexplore.exe");
            else if (app.Title == "Phone")
                fileNames.Insert(0, "\\Windows\\Start Menu\\Programs\\Phone.lnk");

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

            format.Dispose();
            textBrush.Dispose();

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

            runningWindows.Clear();
            windows.Clear();
            //adding exceptions:
            windows.Add("MS_SIPBUTTON");
            foreach (AppDetails app in Config.Applications)
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
            MenuItem menu = ((MenuItem)sender);
            RunningApp app = runningWindows[menu.Parent.MenuItems.IndexOf(menu)];
            Config.AddApplication(app);

            LoadConfiguredApps();
            LoadRunningApps();
        }

        internal void Remover()
        {
            if (appList.SelectedItem != null)
            {
                Config.RemoveApplication(appList.SelectedItem.YIndex);
                LoadConfiguredApps();
                LoadRunningApps();
            }
        }

        private void appList_SelectedItemChanged(object sender, EventArgs e)
        {
            mnuRemove.Enabled = appList.SelectedItem != null;

        }
    }
}
