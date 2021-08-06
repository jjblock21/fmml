﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using JumpinFrog.ProcessChecker;

namespace Fireworks_Mania_Modloader
{
    public partial class Window : Form
    {
        public static string path = Directory.GetCurrentDirectory() + @"\Main.dll";
        private static Process process;
        private static ProcessChecker checker;
        private IntPtr injected;

        // This is part of an Easteregg 'c' :) :D ^^ idk why so many Smilies
        // Here, have another one: 'u'
        // Why am I spening my time on this? OwO
        private int reloadCounter = 0;

        public Window()
        {
            InitializeComponent();
        }

        private void Start(object sender, EventArgs e)
        {
            versionLabel.Text = Program.version;
            UpdateLabel();
            LoadProcesses();
        }

        private void window_KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void Exit(object sender, FormClosingEventArgs e)
        {

        }

        private void Window_Resize(object sender, EventArgs e)
        {

        }

        private void UpdateLabel()
        {
            if (DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day == 24)
                statusLabel.Text = "Merry Christmas!";
            else if (DateTime.UtcNow.Month == 12 && DateTime.UtcNow.Day == 25)
                statusLabel.Text = "Merry Christmas!";
            else if (DateTime.UtcNow.Month == 10 && DateTime.UtcNow.Day == 31)
            {
                if (new Random().NextDouble() < 0.5f)
                    statusLabel.Text = "Boo!";
                else statusLabel.Text = "OooOo Spooky!";
            }
            else if (DateTime.UtcNow.Month == 7 && DateTime.UtcNow.Day == 4)
            {
                if (new Random().NextDouble() < 0.75f)
                    statusLabel.Text = "Independence Day!";
                else statusLabel.Text = "Got Fireworks?";
            }
            else statusLabel.Text = "";
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
            reloadCounter++;
            LoadProcesses();
            if (reloadCounter == 21)
                statusLabel.Text = "You refreshed 21 times!";
            else if (reloadCounter == 69)
                statusLabel.Text = "You refreshed 69 times, nice.";
            else statusLabel.Text = "";
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

        private void status_Click(object sender, EventArgs e)
        {

        }

        private void simpleGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void discordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            More moreWindow = new More();
            moreWindow.ShowDialog();
        }

        private void refreshButton_MouseEnter(object sender, EventArgs e)
        {
            refreshButton.FlatAppearance.BorderColor = Color.Silver;
        }

        private void refreshButton_MouseLeave(object sender, EventArgs e)
        {
            refreshButton.FlatAppearance.BorderColor = Color.White;
        }

        private void discordLink_MouseEnter(object sender, EventArgs e)
        {
            moreLink.LinkColor = Color.Gainsboro;
        }

        private void discordLink_MouseLeave(object sender, EventArgs e)
        {
            moreLink.LinkColor = Color.White;
        }
    }
}