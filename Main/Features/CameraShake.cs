using FireworksMania.Common;
using Helpers;
using Main.EnvironmentObserver;
using UnityEngine;

namespace Main.Features
{
    internal class CameraShake
    {
        private static Vector3[,] originalValues;

        //TODO: Maybe not freeze all the shakeables.

        public static void SaveOriginalValues()
        {
            if (MapManager.PlayableMapLoaded)
            {
                ShakeableTransform[] shakeables = Object.FindObjectsOfType<ShakeableTransform>();
                originalValues = new Vector3[shakeables.Length, 2];
                for (int i = 0; i < shakeables.Length; i++)
                {
                    GameReflector gameReflector = new GameReflector(shakeables[i]);
                    originalValues[i, 0] = (Vector3)gameReflector.GetFieldValue("maximumTranslationShake");
                    originalValues[i, 1] = (Vector3)gameReflector.GetFieldValue("maximumAngularShake");
                }
            }
        }

        public static void UpdateCameraShake(bool enableCameraShake)
        {
            if (MapManager.PlayableMapLoaded)
            {
                ShakeableTransform[] shakeables = Object.FindObjectsOfType<ShakeableTransform>();
                int index = 0;
                foreach (ShakeableTransform shakeable in shakeables)
                {
                    GameReflector gameReflector = new GameReflector(shakeable);
                    if (enableCameraShake)
                    {
                        gameReflector.SetFieldValue("maximumTranslationShake", originalValues[index, 0]);
                        gameReflector.SetFieldValue("maximumAngularShake", originalValues[index, 0]);
                        return;
                    }
                    gameReflector.SetFieldValue("maximumTranslationShake", Vector3.zero);
                    gameReflector.SetFieldValue("maximumAngularShake", Vector3.zero);
                    index++;
                }
            }
        }
    }
}
