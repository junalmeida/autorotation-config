using System;

using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace AutoRotationConfig
{
    class Htc : RotationConfig
    {

        const string RegPath = "Software\\HTC\\HTCSENSOR\\GSensor\\WhiteList";
        const string RegPathLocations = "Software\\HTC\\HTCSENSOR\\GSensor\\WhiteList\\Locations";

        internal override Device Device
        {
            get { return Device.Htc; }
        }

        internal Htc()
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
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
            key = key.OpenSubKey(RegPath, write);
#if DEBUG
            if (key == null)
            {
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegPath);
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, write);
            }
#endif
            if (key == null)
                throw new NotSupportedException("Built-in rotation support was not found on this device.");

            return key;
        }

        private Microsoft.Win32.RegistryKey GetKeyLocations(bool write)
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
            key = key.OpenSubKey(RegPathLocations, write);

            if (key == null)
            {
                Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegPathLocations);
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPathLocations, write);
            }

            return key;
        }

        internal override AppDetails[] Applications
        {
            get
            {
                List<AppDetails> lista = new List<AppDetails>();
                using (RegistryKey key = GetKey(false))
                using (RegistryKey keyLocations = GetKeyLocations(false))
                {
                    try
                    {
                        foreach (string data in key.GetValueNames())
                        {
                            object value = key.GetValue(data);
                            if (value != null)
                            {
                                AppDetails app = new AppDetails() { Title = data };
                                app.ClassName = (string)key.GetValue(data);
                                string loc = (string)keyLocations.GetValue(data);
                                if (!string.IsNullOrEmpty(loc))
                                    app.PossibleLocations.Add(loc);
                                lista.Add(app);
                            }
                        }
                        lista.Sort(new Comparison<AppDetails>(delegate(AppDetails a, AppDetails b)
                        {
                            return string.Compare(a.Title, b.Title);
                        }));
                        return lista.ToArray();
                    }
                    finally
                    {
                        key.Close(); keyLocations.Close();
                    }
                }
            }
        }

        internal override void AddApplication(RunningApp app)
        {
            using (RegistryKey key = GetKey(true))
            using (RegistryKey keyLocations = GetKeyLocations(true))
            {
                try
                {

                    key.SetValue(app.Title, app.ClassName, RegistryValueKind.String);
                    keyLocations.SetValue(app.Title, app.Process.FileName);
                }
                finally
                {
                    key.Close(); keyLocations.Close();
                }

            }

        }
        internal override void RemoveApplication(int index)
        {
            using (RegistryKey key = GetKey(true))
            using (RegistryKey keyLocations = GetKeyLocations(true))
            {
                try
                {
                    AppDetails app = Applications[index];
                    key.DeleteValue(app.Title);
                    keyLocations.DeleteValue(app.Title);
                }
                catch
                { }
                finally
                {
                    key.Close(); keyLocations.Close();
                }
            }
        }

        #region ReloadRotationSupport 
        /*
HANDLE GetServiceHandle(
  LPWSTR szPrefix,
  LPWSTR szDllName,
  DWORD pdwDllBuf
);
 * 
BOOL DeregisterService(
  HANDLE hDevice
);
         
HANDLE RegisterService(
  LPCWSTR lpszType,
  DWORD dwIndex,
  LPCWSTR lpszLib,
  DWORD dwInfo
);
*/

        [DllImport("coredll")]
        private extern static int ActivateService(string lpszDevKey, int dwClientInfo);
        [DllImport("coredll")]
        private extern static int GetServiceHandle(string szPrefix, string szDllName, int pdwDllBuf);
        [DllImport("coredll")]
        private extern static bool DeregisterService(int hDevice);

        internal override bool ReloadRotationSupport()
        {
            const string prefix = "SSS";
            const string name = "SmiSensor";

            int handle = GetServiceHandle(prefix + "0:", null, 0);
            if (handle > 0)
            {
                DeregisterService(handle);
                handle = ActivateService(name, 0);
                return handle > 0;
            }
            else
                return false;
        }
        #endregion
    }
}
