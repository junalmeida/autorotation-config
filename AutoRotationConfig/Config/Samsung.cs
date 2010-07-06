using System;

using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Tenor.Mobile.Diagnostics;

namespace AutoRotationConfig
{
    class Samsung : RotationConfig
    {

        const string RegPath = "Software\\AutoRotation";
        const string CountValue = "Count";
        const string DisabledValue = "disable";
        const string ProcessName = "\\Windows\\RotationSupport.exe";


        internal override Device Device
        { get { return Device.Samsung; } }

        internal Samsung()
        {
            CheckKey();
        }


        private void CheckKey()
        {
            using (Microsoft.Win32.RegistryKey key = GetKey(false))
            {
            }
        }

        private Microsoft.Win32.RegistryKey GetKey(bool write)
        {
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
            key = key.OpenSubKey(RegPath, write);
#if DEBUG
            if (key == null)
            {
                Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegPath);
                key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegPath, write);
            }
#endif
            if (key == null)
                throw new NotSupportedException("Built-in rotation support was not found on this device.");

            return key;
        }

        
        

        internal override bool Enabled
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





        internal int TotalCount
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


        internal override AppDetails[] Applications
        {
            get
            {
                List<AppDetails> lista = new List<AppDetails>();
                RegistryKey key = GetKey(false);
                try
                {
                    for (int i = 0; i < TotalCount; i++)
                    {
                        object value = key.GetValue(i.ToString());
                        if (value != null)
                        {
                            AppDetails app = new AppDetails() { Title = value.ToString() };
                            value = key.GetValue(app.Title);
                            if (value != null)
                                app.PossibleLocations.AddRange(value.ToString().Split(','));
                            lista.Add(app);
                        }
                    }
                    return lista.ToArray();
                }
                finally { key.Close(); }

            }
        }


        internal override bool ReloadRotationSupport()
        {
            IList<Process> list = Tenor.Mobile.Diagnostics.Process.GetProcesses();
            foreach (Tenor.Mobile.Diagnostics.Process p in list)
            {
                if (p.FileName.ToLower() == ProcessName.ToLower())
                {
                    try
                    {
                        p.Kill();
                        System.Threading.Thread.Sleep(2000);
                        Process.Start(p.FileName, string.Empty);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

            }
            return false;
        }



        internal override void AddApplication(RunningApp app)
        {
            RegistryKey key = GetKey(true);
            try
            {

                int index = Applications.Length;

                key.SetValue(index.ToString(), app.Title, RegistryValueKind.String);

                key.SetValue(app.Title, app.Process.FileName);

                TotalCount = index + 1;
            }
            catch { throw; }
            finally { key.Close(); }

        }

        internal override void RemoveApplication(int index)
        {
            RegistryKey key = GetKey(true);
            try
            {
                List<AppDetails> currentApps = new List<AppDetails>(Applications);
                int indexToRemove = currentApps.Count - 1;
                currentApps.RemoveAt(index);

                for (int i = 0; i < currentApps.Count; i++)
                    key.SetValue(i.ToString(), currentApps[i].Title, RegistryValueKind.String);

                try
                {
                    key.DeleteValue(indexToRemove.ToString());
                }
                catch { }
                TotalCount = currentApps.Count;
            }
            finally { key.Close(); }
        }


    }
}
