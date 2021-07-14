using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Injected
{
    public class GameReflector
    {
        public const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public const BindingFlags FIELD_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private object obj;
        private Type type;

        public GameReflector(object obj)
        {
            this.obj = obj;
            type = obj.GetType();
        }

        public MethodInfo GetMethod(string method)
        {
            return type.GetMethod(method, FLAGS);
        }

        public FieldInfo GetField(string field)
        {
            return type.GetField(field, FIELD_FLAGS);
        }

        public object InvokMethod(MethodInfo method, params object[] parameters)
        {
            return method.Invoke(obj, parameters);
        }

        public object InvokMethod(string method, params object[] parameters)
        {
            return GetMethod(method).Invoke(obj, parameters);
        }

        public object GetFieldValue(FieldInfo field)
        {
            return field.GetValue(obj);
        }

        public object GetFieldValue(string field)
        {
            return GetField(field).GetValue(obj);
        }

        public void SetFieldValue(FieldInfo field, object value)
        {
            field.SetValue(obj, value);
        }

        public void SetFieldValue(string field, object value)
        {
            GetField(field).SetValue(obj, value);
        }

        public object Object { get => obj; }
        public Type Type { get => type; }
        public Assembly Assembly { get => type.Assembly; }
    }
}
