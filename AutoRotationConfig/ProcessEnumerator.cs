using System;
using System.Runtime.InteropServices;

using System.Collections.Generic;
using System.Text;

namespace AutoRotationConfig
{

    public class ProcessEnumerator
    {

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

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

        public static string GetWindowClass(IntPtr handle)
        {
            StringBuilder sb = new StringBuilder(255);
            GetClassName(handle, sb, sb.Capacity);
            return sb.ToString().Trim();
        }

    }
}