using Main.Setup;
using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            Load = new GameObject("Mod");
            AttachToGame.AttachAll(Load);
            Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Disable()
        {
            Object.Destroy(Load);
            AttachToGame.DestroyAll(Load);
        }

        private static GameObject Load;
    }
}
