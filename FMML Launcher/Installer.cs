using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FMML_Launcher
{
    public static class Installer
    {
        public static Task Download()
        {
            WebClient wc = new WebClient();
            Task task = wc.DownloadFileTaskAsync(
                Folders.current_mod_file_url(),
                Folders.mod_update_tmp_path()
            );
            wc.Dispose();
            return task;
        }

        public static void UnpackAndCopy()
        {
            string modUpdatePath = Folders.mod_update_path();
            if (!Directory.Exists(modUpdatePath))
                Directory.CreateDirectory(modUpdatePath);
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(modUpdatePath);
                foreach (FileInfo file in dirInfo.GetFiles()) file.Delete();
            }
            ZipFile.ExtractToDirectory(Folders.mod_update_tmp_path(), modUpdatePath);
        }

        public static void CleanUp()
        {
            File.Delete(Folders.mod_update_tmp_path());
            UpdateFile.ResetFile();
        }
    }
}
