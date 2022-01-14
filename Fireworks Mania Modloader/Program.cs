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
        public const string Version = "1.1.0-beta";
        public const bool ShowVersionWarning = false;
        public const string TargetProcessName = "Fireworks Mania";
        public const string GameLaunchCommand = @"steam://rungameid/1079260";


        public static Window window;

        [STAThread]
        static void Main()
        {
            Init();
            if (ShowVersionWarning)
            {
                MessageBox.Show(
                    "Warning!\nThis version of FMML only supports" + " the version v2021.10.1 or newer.",
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Information
                );
            }
            Run();
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

        private static void Run()
        {
            window = new Window();
            Application.Run(window);
        }
    }
}
