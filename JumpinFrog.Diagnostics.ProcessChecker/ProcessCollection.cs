using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace JumpinFrog.ProcessChecker
{
    public class ProcessCollection : IComparable<Process>, IEnumerable<Process>, IReadOnlyCollection<Process>, IReadOnlyList<Process>
    {
        private Process[] processes = null;

        public int Count => processes.Length;
        public bool isEmpty => processes == null;

        public Process this[int index] => processes[index];

        public ProcessCollection(Process[] processes)
        {
            this.processes = processes;
        }

        public int CompareTo(Process other)
        {
            if (other != null)
            {
                foreach (Process p in processes)
                {
                    if (p != null)
                        if (p == other) return 1;
                    else return -1;
                }
                return 0;
            }
            else return -1;
        }

        public IEnumerator<Process> GetEnumerator()
        {
            foreach (Process p in processes)
                yield return p;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Process p in processes)
                yield return p;
        }

        public void ForEach(Action<Process, int> action)
        {
            for(int i = 0; i < processes.Length; i++)
                action(processes[i], i);
        }

        public void ForEach(Action<Process> action)
        {
            for (int i = 0; i < processes.Length; i++)
                action(processes[i]);
        }
    }
}
