using Main.Setup;
using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            //DebugConsole.AttachConsole();
            Application.runInBackground = true;
            Load = new GameObject("Mod");
            AttachToGame.AttachAll(Load);
            Load.transform.parent = null;
            Load.hideFlags = HideFlags.DontSave;
            Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Disable() => Object.Destroy(Load);

        private static GameObject Load;
    }
}
