using DG.Tweening;
using FireworksMania.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public static class Utilities
    {
        private static Dictionary<string, GameObject> clones = new Dictionary<string, GameObject>();
        public static string version { get => modVersion + " - " + Application.version; }

        public static readonly string modVersion = "1.1.0-beta";
        public static RaycastHit DoRaycast(Vector3 dir, Vector3 origin)
        {
            Ray ray = new Ray(origin, dir);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }

        public static RaycastHit DoRaycastThroughScreen(Camera cam, Vector2 point)
        {
            Ray ray = cam.ScreenPointToRay(point);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }

        public static RaycastHit DoScreenRaycast(Camera cam)
        {
            return DoRaycastThroughScreen(cam, new Vector2(Screen.width / 2, Screen.height / 2));
        }

        public static string AddClone(GameObject obj)
        {
            clones.Add(obj.name, obj);
            return obj.name;
        }

        public static bool TryGetClone(string name, out GameObject obj) =>
            clones.TryGetValue(name, out obj);

        public static void SetGameState(uint gameState) =>
            InputManager.Instance.SetContext((InputContext)gameState);

        public static Material GetUnlitMaterial(Color color)
        {
            var shader = Shader.Find("Particles/Standard Unlit");
            var material = new Material(shader);
            material.color = color;
            return material;
        }

        public static GUIStyle CreateColorGUIStyle(Color color)
        {
            var style = new GUIStyle();
            style.normal.textColor = color;
            return style;
        }

        public static GUIStyle CreateColorGUIStyle(Color color, int fontSize)
        {
            var style = new GUIStyle();
            style.normal.textColor = color;
            style.fontSize = fontSize;
            return style;
        }

        public static Color GetRandomColor() => Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);

        public static int GetRandomHash()
        {
            return System.DateTime.Now.Ticks.ToString().GetHashCode();
        }

        public static Vector3 GetRandomIntVector(System.Random rand, int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
        {
            int x = rand.Next(minX, maxX);
            int y = rand.Next(minY, maxY);
            int z = rand.Next(minZ, maxZ);
            return new Vector3(x, y, z);
        }

        public static Vector3 GetRandomIntVector(System.Random rand, int min, int max)
        {
            int x = rand.Next(min, max);
            int y = rand.Next(min, max);
            int z = rand.Next(min, max);
            return new Vector3(x, y, z);
        }

        public static Vector3 GetRandomIntVector(System.Random rand, int min, int max, int y)
        {
            int x = rand.Next(min, max);
            int z = rand.Next(min, max);
            return new Vector3(x, y, z);
        }
    }
}
