using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Tenor.Mobile.Diagnostics;

namespace AutoRotationConfig
{
    public enum Device
    {
        Samsung,
        Htc
    }

    internal abstract class RotationConfig
    {

        private static string[] supportedSamsungDevices = new string[] { "GT-I8000", "SCH-I920" };
        private static string[] supportedHtcDevices = new string[] { };
        public static RotationConfig Create()
        {

            //RegistryKey key = Registry.LocalMachine.OpenSubKey("Ident");
            string origName = Tenor.Mobile.Device.Device.OemInfo;
            //if (key != null)
                //origName = key.GetValue("OrigName").ToString().ToUpper();

            foreach (string device in supportedSamsungDevices)
            {
                if (object.Equals(origName, device))
                    return new Samsung();
            }

            foreach (string device in supportedHtcDevices)
            {
                if (object.Equals(origName, device))
                    return new Htc();
            }
            throw new NotSupportedException("This device ('{0}') is not supported by this application. Please, send a feature request.");
        }



        public abstract Device Device { get; }

        protected abstract RegistryKey GetKey(bool write);

        public abstract AppDetails[] Applications { get; }


        public virtual bool FirstTime
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

        public abstract bool ReloadRotationSupport();


        public abstract void AddApplication(RunningApp app);
        public abstract void RemoveApplication(int index);


    }
}
