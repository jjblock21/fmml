using System.Windows.Forms;
using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            UnityEngine.Application.runInBackground = true;
            Loader.Load = new GameObject("Mod");
            Loader.Load.AddComponent<ModMain>();
            Loader.Load.transform.parent = null;
            Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Disable() => Object.Destroy(Load);

        private static GameObject Load;
    }
}
