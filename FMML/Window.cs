using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fireworks_Mania_Modloader
{
    public partial class Window : Form
    {
        public static string path = Directory.GetCurrentDirectory() + @"\Main.dll";
        private static Process process;
        private IntPtr injected;

        public Window()
        {
            InitializeComponent();
        }

        private void Start(object sender, EventArgs e)
        {
            versionLabel.Text = "v" + Program.Version;
            UpdateLabel();
            LoadProcesses();
            EnterRefreshLoop();
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
                var fb = InjectionUtil.Inject(process, path, out IntPtr value);
                if (fb.WasSuccessful)
                {
                    injected = value;
                    WindowState = FormWindowState.Minimized;
                }
                else
                {
                    FailHandler.HandleException("Failed to inject the assembly.\n" + fb.ErrorDescription);
                    LoadProcesses();
                }
            }
        }

        private void UpdateButtons(bool on)
        {
            injectButton.Enabled = on;
            ejectButton.Enabled = on;
        }

        private void ejectButton_Click(object sender, EventArgs e)
        {
            if (process != null && injected != IntPtr.Zero && injected != new IntPtr())
            {
                var fb = InjectionUtil.Eject(process, injected);
                if (!fb.WasSuccessful)
                {
                    FailHandler.HandleException("Failed to eject the assembly.\n" + fb.ErrorDescription);
                    LoadProcesses();
                }
            }
        }

        private void discordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            More moreWindow = new More();
            moreWindow.ShowDialog();
        }

        private void discordLink_MouseEnter(object sender, EventArgs e)
        {
            moreLink.LinkColor = Color.LightGray;
        }

        private void discordLink_MouseLeave(object sender, EventArgs e)
        {
            moreLink.LinkColor = Color.White;
        }

        private void launchGameButton_Click(object sender, EventArgs e)
        {
            Process.Start(Program.GameLaunchCommand);
        }

        #region Midend
        private void UpdateLabel()
        {
            if (DateTime.UtcNow.Month == 12 && (DateTime.UtcNow.Day == 24 || DateTime.UtcNow.Day == 25))
            {
                statusLabel.Text = "Merry Christmas!";
            }
            else if (DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day == 31)
            {
                if (new Random().NextDouble() < 0.5f)
                    statusLabel.Text = "Boo!";
                else statusLabel.Text = "OooOo Spooky!";
            }
            else if (DateTime.UtcNow.Month == 7 && DateTime.UtcNow.Day == 4)
            {
                statusLabel.Text = "Fireworks!";
            }
            else if (DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day == 31)
            {
                statusLabel.Text = "Happy New Year!";
            }
            else
            {
                if (Program.ThanksText == null) statusLabel.Text = "";
                else statusLabel.Text = Program.ThanksText;
            }
        }

        private void LoadProcesses()
        {
            Process proc = ProcessFinder.GetProcess(Program.TargetProcessName);
            if (proc != null)
            {
                status.Text = "Selected: " + proc.ProcessName + " (" + proc.Id + ")";
                UpdateButtons(true);
            }
            else
            {
                status.Text = "Fireworks Mania is not currently active";
                UpdateButtons(false);
            }
            process = proc;
        }

        private async void EnterRefreshLoop()
        {
            while (true)
            {
                LoadProcesses();
                await Task.Delay(2500);
            }
        }

        #endregion
    }
}