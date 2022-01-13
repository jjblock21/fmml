﻿using FireworksMania.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main.EnvironmentObserver
{
    public static class ModSceneManager
    {
        public static void FindComponents()
        {
            sceneLoadingActions = Object.FindObjectOfType<SceneLoadingActions>();
        }

        private static SceneLoadingActions sceneLoadingActions = null;

        public static void LoadMap(Map map)
        {
            if (sceneLoadingActions == null) return;
            switch (map)
            {
                case Map.Town:
                    sceneLoadingActions.LoadTownMap();
                    break;
                case Map.Ranch:
                    sceneLoadingActions.LoadRanchMap();
                    break;
                case Map.Flat:
                    sceneLoadingActions.LoadFlatMap();
                    break;
                case Map.City:
                    sceneLoadingActions.LoadCityMap();
                    break;
                case Map.Laboratory:
                    sceneLoadingActions.LoadLabMap();
                    break;
            }
        }

        public static int GetActiveSceneIndex()
        {
            /*for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                string n = SceneManager.GetSceneByBuildIndex(i).name;
                Debug.LogError(n + " " + i);
            }*/
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
        Town, Ranch, Flat, Laboratory, City
    }
}