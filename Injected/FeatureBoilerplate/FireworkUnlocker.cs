using FireworksMania.Interactions;
using FireworksMania.Inventory;
using System;

namespace FModApi
{
    public static class FireworkUnlocker
    {
        public static bool UnlockFirework(string id, UnlockTypes unlockType = UnlockTypes.Temporarily)
        {
            var im = UnityEngine.Object.FindObjectOfType<InventoryManager>();
            if (im == null) return false;

            GameReflector reflector = new GameReflector(im);

            var t = reflector.Assembly.GetType("FireworksMania.Common.UnlockEntityEvent");
            var c = t.GetConstructor(new Type[] { typeof(string), typeof(UnlockTypes) });
            var rtrn = c.Invoke(new object[] { id, unlockType });

            reflector.InvokeMethod("UnlockEntityId", parameters: rtrn);

            return true;
        }

        public static KnownFireworks KnownFireworks { get; } = new KnownFireworks();
    }
}
