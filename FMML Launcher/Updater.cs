using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMML_Launcher
{
    public partial class Updater : Form
    {
        private Timer animationTimer = new Timer();
        private bool isOpening = false;
        private bool isClosing = false;

        public Updater()
        {
            InitializeComponent();
            animationTimer.Interval = 1;
            animationTimer.Tick += AnimationTimer_Tick;

        }

        private void Updater_Load(object sender, EventArgs e)
        {
            Opacity = 0;
            isOpening = true;
            animationTimer.Start();

            UpdateFMML();
        }

        private async void UpdateFMML()
        {
            await Task.Delay(500);
            progressBar.Value = 15;
            statusLabel.Text = "Looking for Updates";
            await Task.Delay(300);
            if (UpdateFile.NeedsUpdate())
            {
                progressBar.Value = 30;
                statusLabel.Text = "Downloading";
                await Installer.Download();
                progressBar.Value = 60;
                statusLabel.Text = "Unpacking";
                Installer.UnpackAndCopy();
                await Task.Delay(200);
                progressBar.Value = 80;
                statusLabel.Text = "Cleaning up";
                Installer.CleanUp();
                await Task.Delay(200);
                progressBar.Value = 100;
                statusLabel.Text = "Done";
                await Task.Delay(800);
                Done();
                return;
            }
            progressBar.Value = 100;
            statusLabel.Text = "No Updates available";
            await Task.Delay(500);
            Done();
        }

        private void Done()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Folders.mod_executable_path();
            Process.Start(startInfo);

            isClosing = true;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (isOpening)
            {
                if (Opacity < 1)
                    Opacity += 0.08f;
                else
                {
                    isOpening = false;
                    animationTimer.Stop();
                }
            }
            if (isClosing)
            {
                if (Opacity > 0)
                    Opacity -= 0.1f;
                else
                {
                    isClosing = false;
                    animationTimer.Stop();
                    Close();
                }
            }
        }
    }
}
