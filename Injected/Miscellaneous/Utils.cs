using DG.Tweening;
using FireworksGame.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Injected
{
    public static class Utils
    {
        private static Dictionary<string, GameObject> clones = new Dictionary<string, GameObject>();
        public static string version { get; } = "0.1.7";
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
    }
}
