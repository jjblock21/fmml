using FireworksMania.Core.Behaviors;
using FireworksMania.Core.Behaviors.Fireworks.Parts;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public static class Lighter
    {
        public static void IgniteFirework(GameObject obj)
        {
            if (obj.GetComponent<IIgnitable>() == null) return;
            obj.GetComponent<IIgnitable>().Ignite(2500);
        }

        public static async void IgniteAll(bool delayed, int delay)
        {
            foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
            {
                IgniteFirework(obj);
                if (delayed) await Task.Delay(delay);
            }
        }

        public static void SpawnFire(GameObject obj)
        {
            IFlammable flammeable = obj.GetComponent<IFlammable>();
            if (flammeable != null)
                flammeable.ApplyFireForce(2500f);
        }
    }
}
