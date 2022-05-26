using FireworksMania.Common;
using FireworksMania.Core.Behaviors.Fireworks.Parts;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Interactions;
using FireworksMania.Inventory;
using FireworksMania.Props;
using FireworksMania.ScriptableObjects.EntityDefinitions;
using Helpers;
using Main;
using Main.EnvironmentObserver;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public static class Stuff
    {
        public static void DeleteAll()
        {
            foreach (Rigidbody obj in UnityEngine.Object.FindObjectsOfType<Rigidbody>())
            {
                Delete(obj.GetComponent<Collider>());
            }
        }

        // TODO: Make this better.
        public static void Delete(Collider collider)
        {
            if (collider.gameObject.tag != "MainCamera")
                UnityEngine.Object.Destroy(collider.gameObject);
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

        public static bool UnlockKarlson()
        {
            return FireworkUnlocker.UnlockFirework(FireworkUnlocker.KnownFireworks.KarlsonRocket);
        }

        public static void ClearFireworks()
        {
            FireworksManager.Instance.ClearFireworks();
        }

        public static IEnumerator FuseAll(FuseConnectionType fuseType)
        {
            FuseConnector fuseConnector = new FuseConnector(fuseType);
            IFuseConnectionPoint previousFuseConnectionPoint = null;
            foreach (Rigidbody rigidbody in UnityEngine.Object.FindObjectsOfType<Rigidbody>())
            {
                IHaveFuseConnectionPoint haveFuseConnectionPoint = rigidbody.gameObject.GetComponent<IHaveFuseConnectionPoint>();
                if (haveFuseConnectionPoint == null) continue;
                IFuseConnectionPoint fuseConnectionPoint = haveFuseConnectionPoint.ConnectionPoint;
                if (previousFuseConnectionPoint == null)
                {
                    previousFuseConnectionPoint = fuseConnectionPoint;
                    continue;
                }
                fuseConnector.ConnectWithFuse(fuseConnectionPoint, previousFuseConnectionPoint);
                previousFuseConnectionPoint = fuseConnectionPoint;
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}
