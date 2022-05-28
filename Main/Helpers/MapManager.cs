using FireworksMania.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.EnvironmentObserver
{
    public static class MapManager
    {
        public static void FindComponents()
        {
            sceneLoadingActions = Object.FindObjectOfType<SceneLoadingActions>();
        }

        private static SceneLoadingActions sceneLoadingActions = null;

        public static int GetActiveSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public static bool PlayableMapLoaded
        {
            get => GetActiveSceneIndex() >= 4;
        }

        public static Map GetLoadedMap()
        {
            if (!PlayableMapLoaded) return Map.None;
            switch (GetActiveSceneIndex())
            {
                case 4:
                    return Map.Town;
                case 5:
                    return Map.Ranch;
                case 6:
                    return Map.Flat;
                case 7:
                    return Map.City;
                default:
                    return Map.None;
            }
        }
    }

    public enum Map
    {
        Town, Ranch, Flat, Laboratory, City, None
    }
}
