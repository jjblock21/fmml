using FireworksMania.Common;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Interactions;
using FireworksMania.Inventory;
using FireworksMania.Props;
using FModApi;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Injected
{
    public static class Actions
    {
        public static async void IgniteAll(bool delayed, int delay)
        {
            foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
            {
                if (obj.GetComponent<IIgniteable>() == null) continue;
                obj.GetComponent<IIgniteable>().Ignite(2500);
                if (delayed) await Task.Delay(delay);
            }
        }

        public static void SpawnFire(GameObject obj)
        {
            IFlammable flammeable = obj.GetComponent<IFlammable>();
            if (flammeable != null)
                flammeable.ApplyFireForce(2500f);
        }

        public static void Clone(Collider collider, Vector3 hitPoint)
        {
            if (collider.attachedRigidbody != null)
            {
                GameObject obj = collider.gameObject;
                if (obj.tag != "MainCamera")
                {
                    GameObject clone = UnityEngine.Object.Instantiate(obj) as GameObject;
                    Rigidbody rb = clone.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    clone.transform.position = hitPoint;
                    clone.SetActive(true);
                    Utils.AddClone(clone);
                }
            }
        }

        public static void CrazyClone(Collider collider, Vector3 hitPoint)
        {
            GameObject obj = collider.gameObject;
            if (obj.tag != "MainCamera")
            {
                GameObject clone = UnityEngine.Object.Instantiate(obj) as GameObject;
                clone.AddComponent<Rigidbody>();
                var mesh = obj.GetComponent<MeshFilter>().mesh;
                if (mesh != null)
                {
                    BoxCollider col = obj.AddComponent<BoxCollider>();
                    col.center = mesh.bounds.center;
                }
                Rigidbody rb = clone.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                clone.transform.position = hitPoint;
                clone.SetActive(true);
                Utils.AddClone(clone);
            }
        }

        public static void Newtonify(Collider collider)
        {
            GameObject obj = collider.gameObject;
            if (obj.tag != "MainCamera")
            {
                obj.AddComponent<Rigidbody>();
                var mesh = obj.GetComponent<MeshFilter>().mesh;
                if (mesh != null)
                {
                    BoxCollider col = obj.AddComponent<BoxCollider>();
                    col.center = mesh.bounds.center;
                }
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                obj.SetActive(true);
                Utils.AddClone(obj);
            }
        }

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
            FireworkSpawner.SpawnTim(hit.point + new Vector3(0, 1.25f, 0), obj);
        }

        public static bool UnlockTim()
        {
            return FireworkUnlocker.UnlockFirework(FireworkUnlocker.KnownFireworks.TimShell);
        }
    }
}
