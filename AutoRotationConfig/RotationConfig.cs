using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace AutoRotationConfig
{
    public enum Device
    {
        Samsung,
        Htc
    }

    public class RotationConfig
    {
        public Device Device
        {
            get
            {
#if SAMSUNG
                return Device.Samsung;
#else
                return Device.Htc;
#endif
            }
        }
#if SAMSUNG
        const string RegPath = "Software\\AutoRotation";
        const string CountValue = "Count";
        const string DisabledValue = "disable";
        const string ProcessName = "RotationSupport.exe";
#else

#endif


        private RegistryKey GetKey(bool write)
        {
#if SAMSUNG
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
#else
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
#endif
            key = key.OpenSubKey(RegPath, write);
#if DEBUG
            if (key == null)
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegPath);
                key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath, write);
            }
#endif
            if (key == null)
                throw new NotSupportedException(string.Format("This application only supports {0} devices.", Device.ToString()));


            return key;
        }

        public bool FirstTime
        {
            get
            {
                RegistryKey key = GetKey(false);
                try
                {
                    object val = key.GetValue("_FirstTime");
                    return (val == null ? true : Convert.ToBoolean(val));
                }
                catch { throw; }
                finally { key.Close(); }
            }
            set
            {
                RegistryKey key = GetKey(true);
                try
                {
                    key.SetValue("_FirstTime", Convert.ToInt32(value), RegistryValueKind.DWord);
                }
                catch { throw; }
                finally { key.Close(); }
            }
        }


        public bool Enabled
        {
            get
            {
                RegistryKey key = GetKey(false);
                try
                {
                    object disable = key.GetValue(DisabledValue);
                    return (disable == null ? !false : !Convert.ToBoolean(disable));
                }
                catch { throw; }
                finally { key.Close(); }
            }
            set
            {
                RegistryKey key = GetKey(true);
                try
                {
                    key.SetValue(DisabledValue, Convert.ToInt32(!value), RegistryValueKind.DWord);
                }
                catch { throw; }
                finally { key.Close(); }
            }
        }

         
        public int TotalCount
        {
            get
            {
                RegistryKey key = GetKey(false);
                try
                {
                    object countO = key.GetValue(CountValue);
                    return (countO == null ? 0 : (int)countO);
                }
                catch { throw; }
                finally { key.Close(); }
            }
            set
            {
                RegistryKey key = GetKey(true);
                try
                {
                    key.SetValue(CountValue, value, RegistryValueKind.DWord);
                }
                catch { throw; }
                finally { key.Close(); }
            }
        }


        public string[] Applications
        {
            get
            {
                List<string> lista = new List<string>();
                RegistryKey key = GetKey(false);
                try
                {
                    for (int i = 0; i < TotalCount; i++)
                    {
                        object value = key.GetValue(i.ToString());
                        if (value != null)
                            lista.Add(value.ToString());
                    }
                    return lista.ToArray();
                }
                catch { throw; }
                finally { key.Close(); }

            }
        }



        public void ReloadRotationSupport()
        {
            IList<OpenNETCF.ToolHelp.ProcessEntry> list = OpenNETCF.ToolHelp.ProcessEntry.GetProcesses();
            foreach (OpenNETCF.ToolHelp.ProcessEntry p in list)
            {
                if (p.ExeFile.ToLower() == ProcessName.ToLower())
                {
                    p.Kill();
                    System.Threading.Thread.Sleep(2000);
                    Process.Start("\\windows\\" + ProcessName, string.Empty);
                    break;
                }
            }
        }

        public void AddApplication(string title)
        {
            RegistryKey key = GetKey(true);
            try
            {

                int index = Applications.Length;

                key.SetValue(index.ToString(), title, RegistryValueKind.String);


                TotalCount = index + 1;
            }
            catch { throw; }
            finally { key.Close(); }

        }

        public void RemoveApplication(int index)
        {
            RegistryKey key = GetKey(true);
            try
            {
                List<string> currentApps = new List<string>(Applications);
                int indexToRemove = currentApps.Count - 1; 
                currentApps.RemoveAt(index);

                for (int i = 0; i < currentApps.Count; i++ )
                    key.SetValue(i.ToString(), currentApps[i], RegistryValueKind.String);
                
                try
                {
                    key.DeleteValue(indexToRemove.ToString());
                }
                catch { }
                TotalCount = currentApps.Count;
            }
            catch { throw; }
            finally { key.Close(); }
        }
    }
}
