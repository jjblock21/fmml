using FireworksMania.Fireworks.Parts;
using FireworksMania.Props;
using FireworksMania.ScriptableObjects.EntityDefinitions;
using FModApi;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public static class Lighter
    {
        public static void IgniteFirework(GameObject obj)
        {
            if (obj.GetComponent<IIgniteable>() == null) return;
            obj.GetComponent<IIgniteable>().Ignite(2500);
        }

        public static async void IgniteAll(bool delayed, int delay)
        {
            foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
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
    }
}
