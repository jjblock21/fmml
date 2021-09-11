using Injected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public static class Cloner
    {
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
    }
}
