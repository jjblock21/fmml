using Main.Setup;
using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            ModHost = new GameObject("Mod");
            AttachToGame.AttachAll(ModHost);
            Object.DontDestroyOnLoad(Loader.ModHost);
        }

        public static void Disable()
        {
            Object.Destroy(ModHost);
            AttachToGame.DestroyAll(ModHost);
        }

        private static GameObject ModHost;
    }
}
