using System;

using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoRotationConfig
{

    public class ToolHelp
    {
        [DllImport("Toolhelp.dll", SetLastError = true, CharSet =
        CharSet.Auto)]
        private static extern IntPtr
        CreateToolhelp32Snapshot(Enums.SnapshotFlags dwFlags, IntPtr th32ProcessID);

        [DllImport("Toolhelp.dll", SetLastError = true, CharSet =
        CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32First(IntPtr hSnapshot, ref
Structs.PROCESSENTRY32 lppe);

        [DllImport("Toolhelp.dll", SetLastError = true, CharSet =
        CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool Process32Next(IntPtr hSnapshot, ref
Structs.PROCESSENTRY32 lppe);

        [DllImport("Toolhelp.dll", SetLastError = true, CharSet =
        CharSet.Auto)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseToolhelp32Snapshot(IntPtr hSnapshot);

        public class Enums
        {
            [Flags]
            public enum SnapshotFlags : uint
            {
                //Indicates that the snapshot handle is to be inheritable.
                TH32CS_INHERIT = 0x80000000,

                //Includes all processes and threads in the system, plus the
                //heaps and modules of the process specified in th32ProcessID.
                //Equivalent to specifying the TH32CS_SNAPHEAPLIST,
                //TH32CS_SNAPMODULE, TH32CS_SNAPPROCESS, and TH32CS_SNAPTHREAD
                //values combined using an OR operation ('|').
                TH32CS_SNAPALL = 0x0000001F,

                //Includes all heaps of the process specified in th32ProcessID
                //in the snapshot. To enumerate the heaps, see Heap32ListFirst.
                TH32CS_SNAPHEAPLIST = 0x00000001,

                //Includes all modules of the process specified in
                //th32ProcessID in the snapshot. To enumerate the modules, see
                //Module32First. 64-bit Windows: Using this flag in a 32-bit
                //process includes the 32-bit modules of the process specified
                //in th32ProcessID, while using it in a 64-bit process includes
                //the 64-bit modules. To include the 32-bit modules of the
                //process specified in th32ProcessID from a 64-bit process, use
                //the TH32CS_SNAPMODULE32 flag.
                TH32CS_SNAPMODULE = 0x00000008,

                //Includes all 32-bit modules of the process specified in
                //th32ProcessID in the snapshot when called from a 64-bit
                //process. This flag can be combined with TH32CS_SNAPMODULE or
                //TH32CS_SNAPALL.
                TH32CS_SNAPMODULE32 = 0x00000010,

                //Includes all processes in the system in the snapshot. To
                //enumerate the processes, see Process32First. 
                TH32CS_SNAPPROCESS = 0x00000002,

                //Includes all threads in the system in the snapshot. To
                //enumerate the threads, see Thread32First. To identify the
                //threads that belong to a specific process, compare its
                //process identifier to the th32OwnerProcessID member of the
                //THREADENTRY32 structure when enumerating the threads.
                TH32CS_SNAPTHREAD = 0x00000004,

                //By default, the process heap information is included when
                //creating PROCESS snapshot. For a more efficient way of
                //receiving the basic information of the process, use this flag
                //with TH32CS_SNAPPROCESS.
                TH32CS_SNAPNOHEAPS = 0x40000000
                //
                //
                //
                //HeapList = 0x00000001,
                //Process = 0x00000002,
                //Thread = 0x00000004,
                //Module = 0x00000008,
                //Module32 = 0x00000010,
                //Inherit = 0x80000000,
                //NoHeaps = 0x40000000,
                //All = 0x0000001F
            }
        }

        public class Structs
        {

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            public struct PROCESSENTRY32
            {
                public uint dwSize;
                public uint cntUsage;
                public uint th32ProcessID;
                public IntPtr th32DefaultHeapID;
                public uint th32ModuleID;
                public uint cntThreads;
                public uint th32ParentProcessID;
                public int pcPriClassBase;
                public uint dwFlags;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szExeFile;
            }
        }




        public static List<ToolHelp.Structs.PROCESSENTRY32> GetRunningProcesses()
        {
            List<ToolHelp.Structs.PROCESSENTRY32> retVal = new List<ToolHelp.Structs.PROCESSENTRY32>();
            IntPtr snapshot = IntPtr.Zero;

            Structs.PROCESSENTRY32 processEntry = new Structs.PROCESSENTRY32();
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();

            try
            {
                snapshot = ToolHelp.CreateToolhelp32Snapshot(
                ToolHelp.Enums.SnapshotFlags.TH32CS_SNAPPROCESS |
                ToolHelp.Enums.SnapshotFlags.TH32CS_SNAPNOHEAPS,
                IntPtr.Zero);

                processEntry.dwSize = (uint)
                Marshal.SizeOf(typeof(ToolHelp.Structs.PROCESSENTRY32)) + 8;

                if (ToolHelp.Process32First(snapshot, ref processEntry))
                {
                    do
                    {
                        retVal.Add(processEntry);

                        processEntry = new
                        ToolHelp.Structs.PROCESSENTRY32();
                        processEntry.dwSize = (uint)
                        Marshal.SizeOf(typeof(ToolHelp.Structs.PROCESSENTRY32)) + 8;

                    } while (ToolHelp.Process32Next(snapshot, ref processEntry));

                }
                else
                {
                    int error = Marshal.GetLastWin32Error();
                    throw new Exception(string.Format("Error {0}.", error));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (snapshot != IntPtr.Zero)
                {
                    ToolHelp.CloseToolhelp32Snapshot(snapshot);
                }
            }
            return retVal;
        }
    }
}