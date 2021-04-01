using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DarkUI.Forms;
using JumpinFrog.ProcessChecker;

namespace Fireworks_Mania_Modloader
{
    public partial class Window : DarkForm
    {
        public static string path = Directory.GetCurrentDirectory() + @"\Injected.dll";
        private static Process process;
        private static ProcessChecker checker;
        private IntPtr injected;

        public Window()
        {
            InitializeComponent();
        }

        private void Start(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void Exit(object sender, FormClosingEventArgs e)
        {

        }

        private void Window_Resize(object sender, EventArgs e)
        {

        }

        private void injectButton_Click(object sender, EventArgs e)
        {

            if (process != null)
            {
                if (InjectionUtil.Inject(process, path, out IntPtr value))
                {
                    injected = value;
                    WindowState = FormWindowState.Minimized;
                }
                else
                {
                    FailHandler.HandleException("Failed to inject the assembly.");
                    LoadProcesses();
                }
            }
        }

        private void LoadProcesses()
        {
            checker = new ProcessChecker("Fireworks Mania");
            if (checker.GetProcess() != null)
            {
                process = checker.GetProcess();
                status.Text = "Selected: " + process.ProcessName + " (" + process.Id + ")";
                UpdateButtons(true);
            }
            else
            {
                status.Text = "Fireworks Mania is not currently active";
                UpdateButtons(false);
            }
        }

        private void UpdateButtons(bool on)
        {
            injectButton.Enabled = on;
            ejectButton.Enabled = on;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void ejectButton_Click(object sender, EventArgs e)
        {
            if (process != null && injected != IntPtr.Zero && injected != new IntPtr())
            {
                if (!InjectionUtil.Eject(process, injected))
                {
                    FailHandler.HandleException("Failed to eject the assembly.");
                    LoadProcesses();
                }
            }
        }
    }
}