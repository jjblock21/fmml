using FireworksMania.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Injected
{
    public class FireworkSpawner
    {
        private const string LOCATION = "_spawnLocation";
        private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private const BindingFlags FIELD_FLAGS = BindingFlags.NonPublic | BindingFlags.Instance;

        public static void SpawnTim(Vector3 pos, MonoBehaviour gobj)
        {
            var obj = UnityEngine.Object.FindObjectOfType<FireworkRespawner>();
            Transform original = (Transform)obj.GetType().GetField(LOCATION, FIELD_FLAGS).GetValue(obj);
            SetPosition(pos, obj);
            gobj.StartCoroutine(RespawnFireworkCoroutine(obj, original));
        }

        private static IEnumerator RespawnFireworkCoroutine(FireworkRespawner obj, Transform original)
        {
            var m1 = obj.GetType().GetMethod("RespawnFirework", FLAGS);
            var m2 = obj.GetType().GetMethod("ShowSpawnEffect", FLAGS);
            var m3 = obj.GetType().GetMethod("HideSpawnEffect", FLAGS);
            m2.Invoke(obj, null);
            yield return new WaitForSeconds(0.25f);
            m3.Invoke(obj, null);
            m1.Invoke(obj, null);
            obj.GetType().GetField(LOCATION, FIELD_FLAGS).SetValue(obj, original);
        }

        private static void SetPosition(Vector3 pos, FireworkRespawner obj)
        {
            Transform ts = (Transform)obj.GetType().GetField(LOCATION, FIELD_FLAGS).GetValue(obj);
            ts.position = pos;
            var h = obj.GetType().GetField(LOCATION, FIELD_FLAGS);
            h.SetValue(obj, ts);
        }
    }
}