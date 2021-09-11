using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Main.Setup
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttachToGameAttribute : Attribute
    {
        public AttachMode attachMode { get; }
        public string name { get; }
        public AttachToGameAttribute(AttachMode mode, string name = null)
        {
            attachMode = mode;
            this.name = name;
        }
    }

    public static class AttachToGame
    {
        private static List<GameObject> ownObjects = new List<GameObject>();
        public static void AttachAll(GameObject obj)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                var attribs = type.GetCustomAttributes(typeof(AttachToGameAttribute), true);
                if (attribs.Length > 0)
                {
                    if (type.BaseType == typeof(MonoBehaviour))
                    {
                        var attachMode = ((AttachToGameAttribute)attribs[0]).attachMode;
                        string name = ((AttachToGameAttribute)attribs[0]).name;
                        switch (attachMode)
                        {
                            case AttachMode.ModObject:
                                obj.AddComponent(type);
                                break;
                            case AttachMode.Own:
                                if (name == null) return;
                                GameObject gameObject = new GameObject(name);
                                gameObject.transform.parent = obj.transform;
                                gameObject.AddComponent(type);
                                ownObjects.Add(gameObject);
                                break;
                        }
                    }
                }
            }
        }

        public static void DestroyAll(GameObject loader)
        {
            foreach (GameObject obj in ownObjects)
                UnityEngine.Object.Destroy(obj);
        }
    }

    public enum AttachMode
    {
        ModObject, Own
    }
}
