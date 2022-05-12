using FireworksMania.Common;
using FireworksMania.Core.Definitions.EntityDefinitions;
using FireworksMania.ScriptableObjects.EntityDefinitions;
using UnityEngine;

namespace Helpers
{

    public class FireworkSpawner
    {

        private static EntityDefinitionDatabase _database;

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

        public static EntityDefinitionDatabase FindDatabase()
        {
            if (_database == null) Debug.LogError("Database was null.");
            return _database;
        }

        public static void FindComponents()
        {
            EntityDefinitionDatabase[] databases = Resources.FindObjectsOfTypeAll<EntityDefinitionDatabase>();
            EntityDefinitionDatabase database = null;
            if (databases.Length > 0) database = databases[0];
            if (database == null) return;
            _database = database;
        }

        public static KnownFireworks KnownFireworks { get; } = new KnownFireworks();
    }
}
