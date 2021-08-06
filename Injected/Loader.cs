using UnityEngine;
using DebugConsole = System.Addons.WinForms.Console.WindowsFormsDebugConsole;

namespace Injected
{
    public static class Loader
    {
        public static readonly string version = "v0.1.10";
        public static void Init()
        {
            //DebugConsole.AttachConsole();
            Application.runInBackground = true;
            Load = new GameObject("Mod");
            Load.AddComponent<ModMain>();
            Load.transform.parent = null;
            Load.hideFlags = HideFlags.DontSave;
            Load.tag = "Loader";
            Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Disable() => Object.Destroy(Load);

        private static GameObject Load;
    }
}
