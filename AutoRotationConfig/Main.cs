using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        private const string RegPath = "Software\\AutoRotation";
        private const string CountValue = "Count";
        private const string ProcessName = "RotationSupport.exe";

        private int TotalCount
        {
            get
            {
                RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath);
                object countO = key.GetValue(CountValue);
                return (countO == null ? 0 : (int)countO);
            }
            set
            {
                RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath, true);
                key.SetValue(CountValue, value, RegistryValueKind.DWord);
            }
        }

        private void LoadApps()
        {
            listBox.Items.Clear();
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath);
#if DEBUG
            if (key == null)
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegPath);
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath);
#endif

            if (key == null)
                throw new NotSupportedException("This application only supports samsung devices.");

            int count = TotalCount;

            for (int i = 0; i < count; i++)
            {
                object value = key.GetValue(i.ToString());
                if (value != null)
                    listBox.Items.Add(value);
            }
            mnuRemove.Enabled = listBox.SelectedIndex >= 0;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ReloadAutoRotate();
        }

        private void ReloadAutoRotate()
        {
            StringBuilder debug = new StringBuilder();

            IList<OpenNETCF.ToolHelp.ProcessEntry> list = OpenNETCF.ToolHelp.ProcessEntry.GetProcesses();
            foreach (OpenNETCF.ToolHelp.ProcessEntry p in list)
            {
                debug.Append(p.ExeFile + "\r\n");
                if (p.ExeFile.ToLower() == ProcessName.ToLower())
                {
                    p.Kill();
                    Process.Start("\\windows\\" + ProcessName, null);
                    break;
                }
            }
            MessageBox.Show(debug.ToString() + list.Count.ToString());
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
            string title = ProcessEnumerator.GetWindowText(handle);
            if (!string.IsNullOrEmpty(title) && !windows.Contains(title) && ProcessEnumerator.IsWindowVisible(handle))
            {
                MenuItem m = new MenuItem();
                m.Text = title.Replace("&", "&&");
                m.Click += new EventHandler(m_Click);
                mnuAdd.MenuItems.Add(m);
                windows.Add(title);
                mnuAdd.Enabled = true;
            }
            return 1;
        }

        void m_Click(object sender, EventArgs e)
        {
            AddTitle(((MenuItem)sender).Text.Replace("&&", "&"));
            ReloadRunningApps();
        }

        private void AddTitle(string title)
        {
            int index = listBox.Items.Count + 1;

            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath, true);
            key.SetValue(index.ToString(), title, RegistryValueKind.String);

            listBox.Items.Add(title);

            TotalCount = listBox.Items.Count;
            timer1.Enabled = true;
        }

        private void mnuRemove_Click(object sender, EventArgs e)
        {
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath, true);
            foreach (string title in listBox.Items)
                key.SetValue(listBox.Items.IndexOf(title).ToString(), title, RegistryValueKind.String);
            try
            {
                key.DeleteValue((TotalCount - 1).ToString());
            }
            catch { }
            TotalCount = listBox.Items.Count;
            timer1.Enabled = true;
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

    }
}