using FireworksMania.Common;
using FireworksMania.Fireworks.Parts;
using FireworksMania.Props;
using System.Threading.Tasks;
using UnityEngine;

namespace Injected
{
    public static class Actions
    {
        public static async void IgniteAll(bool delayed)
        {
            foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
            {
                if (obj.GetComponent<IIgniteable>() == null) continue;
                obj.GetComponent<IIgniteable>().Ignite(2500);
                if (delayed) await Task.Delay(1);
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
                    GameObject clone = Object.Instantiate(obj) as GameObject;
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
            if (collider.attachedRigidbody != null)
            {
                GameObject obj = collider.gameObject;
                if (obj.tag != "MainCamera")
                {
                    collider.enabled = true;
                    collider.isTrigger = false;
                    GameObject clone = Object.Instantiate(obj) as GameObject;
                    Rigidbody rb = clone.AddComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    clone.transform.position = hitPoint;
                    clone.SetActive(true);
                    Utils.AddClone(clone);
                }
            }
        }

        public static void Newtonify(Collider collider)
        {
            if (collider == null) return;
            GameObject obj = collider.gameObject;
            if (obj.tag != "MainCamera")
            {
                collider.enabled = true;
                collider.isTrigger = false;
                var rb = obj.GetComponent<Rigidbody>();
                if (rb == null) rb = obj.AddComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                obj.SetActive(true);
            }
        }

        public static bool DeleteAll()
        {
            foreach (Rigidbody obj in Object.FindObjectsOfType<Rigidbody>())
            {
                try
                {
                    if (obj.tag != "MainCamera")
                        Object.Destroy(obj.gameObject);
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

        public static void SpawnTim(Camera cam, MonoBehaviour obj)
        {
            var hit = Utils.DoRaycastThroughScreenPoint(cam, new Vector2(Screen.width / 2, Screen.height / 2));
            if (hit.collider == null) return;
            FireworkSpawner.SpawnTim(hit.point + new Vector3(0, 1.25f, 0), obj);
        }
    }
}
