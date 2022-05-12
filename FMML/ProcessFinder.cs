using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fireworks_Mania_Modloader
{
    public static class ProcessFinder
    {
        public static Process GetProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
                return processes[0];
            return null;
        }

        public static Process FindProcess(IntPtr handle)
        {
            int id = GetProcessId(handle);
            return Process.GetProcessById(id);
        }

        [DllImport("kernel32.dll")]
        private static extern int GetProcessId(IntPtr handle);
    }
}
