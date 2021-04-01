using System;
using System.Diagnostics;

namespace JumpinFrog.ProcessChecker
{
    public class ProcessChecker
    {
        private int processId = -1;
        private string processName = null;
        private IntPtr handle = IntPtr.Zero;

        public ProcessChecker(int processId)
        {
            this.processId = processId;
        }

        public ProcessChecker(string name)
        {
            processName = name;
        }

        public ProcessChecker(IntPtr handle)
        {
            this.handle = handle;
        }

        private int GetSpecifiedIdentifyerType()
        {
            if (processId != -1)
                return 0;
            else if (processName != null)
                return 1;
            else if (handle != IntPtr.Zero)
                return 2;
            else
                return -1;
        }

        private Process FindProcess(IntPtr handle)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Handle == handle)
                    return p;
            }
            return null;
        }



        public bool IsRuning()
        {
            switch (GetSpecifiedIdentifyerType())
            {
                case 0:
                    if (Process.GetProcessById(processId) != null)
                        return true;
                    break;
                case 1:
                    if (Process.GetProcessesByName(processName)[0] != null)
                        return true;
                    break;
                case 2:
                    if (FindProcess(handle) != null)
                        return true;
                    break;
            }
            return false;
        }

        public ProcessCollection GetProcesses()
        {
            Process[] process = null;
            switch (GetSpecifiedIdentifyerType())
            {
                case 0:
                    if (Process.GetProcessById(processId) != null)
                        process = new Process[] { Process.GetProcessById(processId) };
                    break;
                case 1:
                    if (Process.GetProcessesByName(processName).Length > 0)
                        process = Process.GetProcessesByName(processName);
                    break;
                case 2:
                    if (FindProcess(handle) != null)
                        process = new Process[] { FindProcess(handle) };
                    break;
            }
            return new ProcessCollection(process);
        }

        public Process GetProcess()
        {
            if (GetProcesses().isEmpty)
                return null;
            else return GetProcesses()[0];
        }

        public static bool Is32BitSystem()
        {
            if (IntPtr.Size == 4)
                return true;
            else
                return false;
        }

        public static int OpenProcesses => Process.GetProcesses().Length;
        public static int OpenWindows => WindowHook.GetOpenWindows().Count;
    }
}
