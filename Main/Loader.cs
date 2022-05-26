using UnityEngine;

namespace Main
{
    public static class Loader
    {
        public static void /*This is a very bri'ish comment */Init()
        {
            hostObject = new GameObject("FMML");
            hostObject.AddComponent<ModMain>();
            Object.DontDestroyOnLoad(hostObject);
        }

        public static void Disable()
        {
            Object.Destroy(hostObject);
        }

        private static GameObject hostObject;
    }
}