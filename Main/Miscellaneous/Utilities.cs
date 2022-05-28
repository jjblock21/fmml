using DG.Tweening;
using FireworksMania.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Main
{
    public static class Utilities
    {
        public static string version { get => ModVersion + " - " + Application.version; }

        public const string ModVersion = "1.1.3";
        public const string AppName = "FMML";

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

        public static GUIStyle CreateColorGUIStyle(Color color, int fontSize = -1)
        {
            var style = new GUIStyle();
            style.normal.textColor = color;
            if (fontSize == -1) return style;
            style.fontSize = fontSize;
            return style;
        }

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

        public static Vector3 GetRandomIntVector(System.Random rand, int min, int max, int y)
        {
            int x = rand.Next(min, max);
            int z = rand.Next(min, max);
            return new Vector3(x, y, z);
        }

        public static string SplitPascalCase(string originalString)
        {
            // Definetly not copied from stackoverflow.
            IEnumerable<char> output = originalString.SelectMany(
                (c, i) => i != 0 && char.IsUpper(c)
                && !char.IsUpper(originalString[i - 1])
                ? new char[] { ' ', c } : new char[] { c });
            return new string(output.ToArray());
        }
    }
}