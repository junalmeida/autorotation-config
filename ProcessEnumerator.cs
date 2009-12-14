using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace AutoRotationConfig
{

    public class ProcEntry
    {
        public string ExeName;
        public uint ID;
    }



    public class ProcessEnumerator
    {

        #region Constants
        private const uint TH32CS_SNAPPROCESS = 0x00000002;
        private const int MAX_PATH = 260;
        #endregion


        #region Structs
        public struct PROCESSENTRY
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public uint th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]

            public string szExeFile;
            uint th32MemoryBase;
            uint th32AccessKey;
        }

        #endregion


        #region P/Invoke

        [DllImport("toolhelp.dll")]
        private static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processID);


        [DllImport("toolhelp.dll")]
        private static extern int CloseToolhelp32Snapshot(IntPtr snapshot);


        [DllImport("toolhelp.dll")]
        private static extern int Process32First(IntPtr snapshot, ref PROCESSENTRY processEntry);



        [DllImport("toolhelp.dll")]
        private static extern int Process32Next(IntPtr snapshot, ref PROCESSENTRY processEntry);

        #endregion



        #region public Methods

        public static bool Enumerate(ref List<ProcEntry> list)
        {

            if (list == null)
            {
                return false;
            }
            list.Clear();

            IntPtr snapshot_ = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
            if (snapshot_ == IntPtr.Zero)
            {
                return false;
            }

            PROCESSENTRY entry_ = new PROCESSENTRY();
            entry_.dwSize = (uint)Marshal.SizeOf(entry_);
            if (Process32First(snapshot_, ref entry_) == 0)
            {
                CloseToolhelp32Snapshot(snapshot_);
                return false;
            }


            do
            {
                ProcEntry procEntry = new ProcEntry();
                procEntry.ExeName = entry_.szExeFile;
                procEntry.ID = entry_.th32ProcessID;
                list.Add(procEntry);
                entry_.dwSize = (uint)Marshal.SizeOf(entry_);

            }

            while (Process32Next(snapshot_, ref entry_) != 0);
            CloseToolhelp32Snapshot(snapshot_);
            return true;

        }

        public static bool KillProcess(uint procID)
        {
            try
            {
                System.Diagnostics.Process proc_ = System.Diagnostics.Process.GetProcessById((int)procID);
                proc_.Kill();
                proc_.Dispose();
                return true;
            }
            catch (ArgumentException)
            {
                return false; //process does not exist
            }
            catch (Exception)
            {
                return false; //cannot kill process (perhaps its system process)
            }
        }
        #endregion


        [DllImport("coredll.dll")]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder buf, int nMaxCount);

        [DllImport("coredll.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("coredll.dll", SetLastError = true)]
        private static extern bool EnumWindows(IntPtr lpEnumFunc, uint lParam);

        public delegate int EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        public static void ListWindows(EnumWindowsProc callback)
        {
            IntPtr callbackDelegatePointer;
            callbackDelegatePointer = Marshal.GetFunctionPointerForDelegate(callback);

            EnumWindows(callbackDelegatePointer, 0);
        }

        public static string GetWindowText(IntPtr handle)
        {
            StringBuilder sb = new StringBuilder(255);
            GetWindowText(handle, sb, sb.Capacity);
            return sb.ToString().Trim();
        }


    }
}