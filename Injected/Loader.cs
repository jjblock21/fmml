﻿using UnityEngine;

namespace Injected
{
    public static class Loader
    {
        public static void Init()
        {
            UnityEngine.Application.runInBackground = true;
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