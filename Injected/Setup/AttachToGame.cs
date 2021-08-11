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
        public AttachToGameAttribute(AttachMode mode)
        {
            attachMode = mode;
        }
    }

    public static class AttachToGame
    {
        public static void AttachAll(GameObject obj)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                var attribs = type.GetCustomAttributes(typeof(AttachToGameAttribute), true);
                if (attribs.Length > 0)
                {
                    var attachMode = ((AttachToGameAttribute)attribs[0]).attachMode;
                    if (attachMode == AttachMode.ModObject)
                        obj.AddComponent(type);
                }
            }
        }
    }

    public enum AttachMode
    {
        ModObject
    }
}
