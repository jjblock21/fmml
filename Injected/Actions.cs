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
                catch (Exception e)
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
            catch (Exception e)
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
                string x = "X: " + Math.Round(controller.gameObject.transform.position.x, 2).ToString();
                string y = "Y: " + Math.Round(controller.gameObject.transform.position.y, 2).ToString();
                string z = "Z: " + Math.Round(controller.gameObject.transform.position.z, 2).ToString();
                text = x + " " + y + " " + z;
                return true;
            }
            text = null;
            return false;
        }

        public static bool UnlockTim()
        {
            return FireworkUnlocker.UnlockFirework(FireworkUnlocker.KnownFireworks.TimShell);
        }

        public static void ClearFireworks()
        {
            FireworksManager.Instance.ClearFireworks();
        }
    }
}
