using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Tenor.Mobile.Diagnostics;
using System.IO;

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
        private static string[] supportedHtcDevices = new string[] { "LEO" };

        internal static RotationConfig Create()
        {
            string origName = Tenor.Mobile.Device.Device.OemInfo;
            string manuf = Tenor.Mobile.Device.Device.Manufacturer;

            foreach (string device in supportedSamsungDevices)
            {
                if (object.Equals(origName.ToUpper(), device))
                    return new Samsung();
            }

            if (manuf.ToUpper().IndexOf("HTC") > -1)
            {
                return new Htc();
            }

            throw new NotSupportedException(string.Format("{0} {1} is not supported by this application. Please, send a feature request.", manuf, origName));
        }



        internal abstract Device Device { get; }

        internal abstract AppDetails[] Applications { get; }


        //public virtual bool FirstTime
        //{
        //    get
        //    {
        //        RegistryKey key = GetKey(false);
        //        try
        //        {
        //            object val = key.GetValue("_FirstTime");
        //            return (val == null ? true : Convert.ToBoolean(val));
        //        }
        //        catch { throw; }
        //        finally { key.Close(); }
        //    }
        //    set
        //    {
        //        RegistryKey key = GetKey(true);
        //        try
        //        {
        //            key.SetValue("_FirstTime", Convert.ToInt32(value), RegistryValueKind.DWord);
        //        }
        //        catch { throw; }
        //        finally { key.Close(); }
        //    }
        //}

        internal abstract bool ReloadRotationSupport();


        internal abstract void AddApplication(RunningApp app);
        internal abstract void RemoveApplication(int index);


        internal RotationConfig()
        {
            SetPossibleLocations();
        }


        Dictionary<string, List<string>> possibleLocations;
        private void SetPossibleLocations()
        {
            possibleLocations = new Dictionary<string, List<string>>();


            string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);

            InterateProgramsFolder(programsPath);


            string windowsPath = "\\Windows";

            AddToList("Notes", Path.Combine(windowsPath, "notes.exe"));
            AddToList("Word Mobile", Path.Combine(windowsPath, "pword.exe"));
            AddToList("Excel Mobile", Path.Combine(windowsPath, "pxl.exe"));
            AddToList("PowerPoint Mobile", Path.Combine(windowsPath, "ppt.exe"));
            AddToList("OneNote Mobile", Path.Combine(windowsPath, "OneNoteMobile.exe"));
            AddToList("Outlook E-mail", Path.Combine(windowsPath, "tmail.exe"));
            AddToList("Messaging", Path.Combine(windowsPath, "tmail.exe"));
            AddToList("Pictures & Videos", Path.Combine(programsPath, "Pictures & Videos.lnk"));
            AddToList("Desktop", Path.Combine(windowsPath, "fexplore.exe"));

            AddToList("Phone", Path.Combine(programsPath, "Phone.lnk"));
            AddToList("Telefone", Path.Combine(programsPath, "Telefone.lnk"));

            AddToList("Contacts", Path.Combine(programsPath, "Contacts.lnk"));
            AddToList("Calendar", Path.Combine(programsPath, "Calendar.lnk"));


        }

        private void InterateProgramsFolder(string programsPath)
        {
            foreach (string fileName in Directory.GetFiles(programsPath, "*.lnk"))
            {
                string title = Path.GetFileNameWithoutExtension(fileName);
                AddToList(title, fileName);
            }

            foreach (string folder in Directory.GetDirectories(programsPath))
                InterateProgramsFolder(folder);
        }


        internal string[] GetPossibleLocations(string title)
        {
            if (possibleLocations.ContainsKey(title))
                return possibleLocations[title].ToArray();
            else
                return new string[] { };
        }

        private void AddToList(string key, string value)
        {
            if (!possibleLocations.ContainsKey(key))
                possibleLocations.Add(key, new List<string>());
            if (!possibleLocations[key].Contains(value))
                possibleLocations[key].Add(value);
        }


    }
}
