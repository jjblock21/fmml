using FireworksMania.Common;
using FireworksMania.ScriptableObjects.EntityDefinitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FModApi
{

    public class FireworkSpawner
    {
        #region Tim [Obsolete]
        private const string LOCATION = "_spawnLocation";
        private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private const BindingFlags FIELD_FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private static readonly Vector3 v = new Vector3(49.6f, 1.3f, 33.77f);

        [Obsolete("This only works in older versions of the game.\nUse FireworkUnlocker.UnlockFirework(FireworkUnlocker.KnownFireworks.TimShell)")]
        public static void RespawnTim(Vector3 pos, MonoBehaviour gobj)
        {
            var obj = UnityEngine.Object.FindObjectOfType<FireworkRespawner>();
            SetFireworkPosition(pos, obj);
            gobj.StartCoroutine(RespawnFireworkCoroutine(obj));
        }

        private static IEnumerator RespawnFireworkCoroutine(FireworkRespawner obj)
        {
            var m1 = obj.GetType().GetMethod("RespawnFirework", FLAGS);
            m1.Invoke(obj, null);
            yield return new WaitForSeconds(0.25f);
            Transform original = (Transform)obj.GetType().GetField(LOCATION, FIELD_FLAGS).GetValue(obj);
            original.position = v;
            obj.GetType().GetField(LOCATION, FIELD_FLAGS).SetValue(obj, original);
        }

        private static void SetFireworkPosition(Vector3 pos, FireworkRespawner obj)
        {
            Transform ts = (Transform)obj.GetType().GetField(LOCATION, FIELD_FLAGS).GetValue(obj);
            ts.position = pos;
            var h = obj.GetType().GetField(LOCATION, FIELD_FLAGS);
            h.SetValue(obj, ts);
        }
        #endregion

        public static GameObject SpawnFirework(FireworkEntityDefinition firework, Vector3 position)
        {
            return SpawnFirework(firework, position, Quaternion.identity, true);
        }

        public static GameObject SpawnFirework(FireworkEntityDefinition entity, Vector3 position, Quaternion rotation, bool animate = false)
        {
            return FireworksManager.Instance.Spawn(FindFireworkPrefab(entity), position, rotation, animate);
        }

        public static GameObject FindFireworkPrefab(FireworkEntityDefinition firework)
        {
            return firework.PrefabGameObject;
        }

        public static BaseEntityDefinition GetEntityDefinition(string fireworkId)
        {
            EntityDefinitionDatabase database = FindDatabase();
            return database.GetEntityDefinition(fireworkId);
        }

        private const string databaseFieldName = "_entityDefinitionDatabase";

        public static EntityDefinitionDatabase FindDatabase()
        {
            if (_database == null) Debug.LogError("Database was null.");
            return _database;
        }

        private static EntityDefinitionDatabase _database;

        public static void ChacheComponents()
        {
            FireworksManager manager = FireworksManager.Instance;
            if (manager == null) return;
            EntityDefinitionDatabase[] databases = Resources.FindObjectsOfTypeAll<EntityDefinitionDatabase>();
            EntityDefinitionDatabase database = null;
            if (databases.Length > 0) database = databases[0];
            if (database == null) return;
            _database = database;
        }

        public static KnownFireworks KnownFireworks { get; } = new KnownFireworks();
    }
}