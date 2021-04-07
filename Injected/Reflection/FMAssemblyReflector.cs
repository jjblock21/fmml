using System;
using System.Reflection;

namespace Injected.Reflection
{
    [Obsolete]
    public class FMAssemblyReflector
    {
        private const string ASSEMBLY = "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

        public Assembly GetAssembly() => Assembly.Load(ASSEMBLY);

        public Assembly GetAssemblyByPublicIncludedType<T>() => typeof(T).Assembly;
        public Assembly GetAssemblyByPublicIncludedType(Type type) => type.Assembly;

        public Type GetAssemblyType(string type) => GetAssembly().GetType(type);

        public MethodInfo GetMethod<T>(string name, Type[] overloadTypes) => typeof(T).GetMethod(name, overloadTypes);
        public MethodInfo GetMethod(Type type, string name, Type[] overloadTypes) => type.GetMethod(name, overloadTypes);

        public MethodInfo GetMethod<T>(string name, Type[] overloadTypes, BindingFlags flags) => typeof(T).GetMethod(name, flags, null, overloadTypes, null);
        public MethodInfo GetMethod(Type type, string name, Type[] overloadTypes, BindingFlags flags) => type.GetMethod(name, flags, null, overloadTypes, null);

        public object InvokeMethod(MethodInfo info, Type type, object[] parameters) => info.Invoke(type, parameters);
        public object InvokeMethod<T>(MethodInfo info, object[] parameters) => info.Invoke(typeof(T), parameters);

        public object InvokeStaticGenericMethod(Type type, MethodInfo method, object[] parameters, params Type[] genericTypes)
        {
            MethodInfo genericMethod = method.MakeGenericMethod(genericTypes);
            return genericMethod.Invoke(type, parameters);
        }
    }
}
