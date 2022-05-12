using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            hostObject = new GameObject("Mod");
            hostObject.AddComponent<ModMain>();
            Object.DontDestroyOnLoad(Loader.hostObject);
        }

        public static void Disable()
        {
            Object.Destroy(hostObject);
        }

        private static GameObject hostObject;
    }
}