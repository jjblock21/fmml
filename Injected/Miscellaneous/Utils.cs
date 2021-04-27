using DG.Tweening;
using FireworksMania.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Injected
{
    public static class Utils
    {
        private static Dictionary<string, GameObject> clones = new Dictionary<string, GameObject>();
        private static List<GameObject> lines = new List<GameObject>();
        public static string version { get; } = "0.1.8-pre-v2021.4.4-exp+";
        public static RaycastHit DoRaycast(Vector3 dir, Vector3 origin)
        {
            Ray ray = new Ray(origin, dir);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
        }

        public static RaycastHit DoRaycastThroughScreenPoint(Camera cam, Vector2 point)
        {
            Ray ray = cam.ScreenPointToRay(point);
            Physics.Raycast(ray, out RaycastHit hit);
            return hit;
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

        public static int DrawLine(float thickness, Material material, Vector3 startPos, Vector3 endPos)
        {
            GameObject line = new GameObject();
            line.transform.position = startPos;
            line.AddComponent<LineRenderer>();
            var l = line.GetComponent<LineRenderer>();
            l.material = material;
            l.startWidth = thickness;
            l.endWidth = thickness;
            l.SetPosition(0, startPos);
            l.SetPosition(1, endPos);
            lines.Add(line);
            return lines.Count - 1;
        }

        public static void ClearLines()
        {
            foreach (GameObject obj in lines)
                Object.Destroy(obj);
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

        public static void DestroyLine(int index) => Object.Destroy(lines[index]);

        public static Color GetRandomColor() => Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
    }
}
