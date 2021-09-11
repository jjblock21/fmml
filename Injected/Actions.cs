using FireworksMania.Common;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Interactions;
using FireworksMania.Inventory;
using FireworksMania.Props;
using FireworksMania.ScriptableObjects.EntityDefinitions;
using FModApi;
using Main;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Injected
{
    public static class Actions
    {
        public static bool DeleteAll()
        {
            foreach (Rigidbody obj in UnityEngine.Object.FindObjectsOfType<Rigidbody>())
            {
                try
                {
                    if (obj.tag != "MainCamera")
                        UnityEngine.Object.Destroy(obj.gameObject);
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e.Message + " " + e.StackTrace);
                    return false;
                }
            }
            return true;
        }

        public static bool Delete(Collider collider)
        {
            try
            {
                if (collider.gameObject.tag != "MainCamera")
                    UnityEngine.Object.Destroy(collider.gameObject);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message + " " + e.StackTrace);
                return false;
            }
            return true;
        }

        public static bool TryGetPositionString(out string text, Player controller)
        {
            if (controller != null)
            {
                string x = "X: " + System.Math.Round(controller.gameObject.transform.position.x, 2).ToString();
                string y = "Y: " + System.Math.Round(controller.gameObject.transform.position.y, 2).ToString();
                string z = "Z: " + System.Math.Round(controller.gameObject.transform.position.z, 2).ToString();
                text = x + " " + y + " " + z;
                return true;
            }
            else
            {
                text = null;
                return false;
            }
        }

        [Obsolete]
        public static void SpawnTim(Camera cam, MonoBehaviour obj)
        {
            var hit = Utils.DoRaycastThroughScreenPoint(cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider == null) return;
            FireworkSpawner.RespawnTim(hit.point + new Vector3(0, 1.25f, 0), obj);
        }

        public static bool UnlockTim()
        {
            return FireworkUnlocker.UnlockFirework(FireworkUnlocker.KnownFireworks.TimShell);
        }
    }
}
