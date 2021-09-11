using FireworksMania.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.FModApi
{
    public static class ModSceneManager
    {
        public static void ChacheComponents()
        {
            s = Object.FindObjectOfType<SceneLoadingActions>();
        }

        private static SceneLoadingActions s = null;

        public static void LoadMap(Map map)
        {
            if (s == null) return;
            switch (map)
            {
                case Map.Town:
                    s.LoadTownMap();
                    break;
                case Map.Ranch:
                    s.LoadRanchMap();
                    break;
                case Map.Flat:
                    s.LoadFlatMap();
                    break;
                case Map.Laboratory:
                    s.LoadLabMap();
                    break;
            }
        }

        public static int GetActiveSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public static bool IsPlayableMapLoaded()
        {
            if (GetActiveSceneIndex() >= 4)
                return true;
            return false;
        }
    }

    public enum Map
    {
        Town, Ranch, Flat, Laboratory
    }
}
