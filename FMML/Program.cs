using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fireworks_Mania_Modloader
{
    public static class Program
    {
        public const string Version = "1.2.0";
        public const bool ShowVersionWarning = true;
        public const string TargetProcessName = "Fireworks Mania";
        public const string GameLaunchCommand = @"steam://rungameid/1079260";
        public const string ThanksText = "Thanks for 1,000 Members!";

        public static Window window;

        [STAThread]
        static void Main()
        {
            Init();
            if (ShowVersionWarning)
            {
                MessageBox.Show(
                    "Warning!\nThis version of FMML only supports versions v2022.4.2 and later.",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
            Application.Run(new Window());
        }

        private static void Init()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += ThreadException;
            //DebugConsole.AttachConsole();
        }

        private static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            FailHandler.HandleException("Error: " + e.Exception.Message, stacktrace: "\n" + e.Exception.StackTrace);
            Application.Exit();
        }
    }
}