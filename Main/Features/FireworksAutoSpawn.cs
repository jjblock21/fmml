using FireworksMania.Core.Definitions.EntityDefinitions;
using Helpers;
using Main.EnvironmentObserver;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public class FireworksAutoSpawn
    {
        private int rarity = 10;
        private int tick = 0;
        private bool spawnFromDatabase = false;

        public void SpawnAndIngniteFirework(Vector3 position, string id)
        {
            var firework = (FireworkEntityDefinition)FireworkSpawner.GetEntityDefinition(id);
            IgniteCoroutine(FireworkSpawner.SpawnFirework(firework, position));
        }

        public void SpawnAndIngniteFirework(Vector3 position, FireworkEntityDefinition firework)
        {
            IgniteCoroutine(FireworkSpawner.SpawnFirework(firework, position));
        }

        public void UpdateSpawnAllFireworksSettings(bool spawnAll)
        {
            spawnFromDatabase = spawnAll;
        }

        private async void IgniteCoroutine(GameObject firework)
        {
            await Task.Delay(500);
            Lighter.IgniteFirework(firework);
        }

        public void UpdateFireworkSpawn()
        {
            if (DoTickCheck())
            {
                System.Random random = new System.Random();
                int value = random.Next(1, rarity);
                if (value == rarity / 2) SpawnRandom();
            }
        }

        private void SpawnRandom()
        {
            if (MapManager.IsPlayableMapLoaded())
            {
                System.Random rand = new System.Random();
                Vector3 pos = new Vector3();
                switch (MapManager.GetLoadedMap())
                {
                    case Map.Town:
                        pos = Utilities.GetRandomIntVector(rand, -100, 100, 1);
                        break;
                    case Map.Ranch:
                        pos = Utilities.GetRandomIntVector(rand, -150, 150, 3);
                        break;
                    case Map.Flat:
                        pos = Utilities.GetRandomIntVector(rand, -100, 100, 1);
                        break;
                    case Map.City:
                        pos = Utilities.GetRandomIntVector(rand, -80, 80, 2);
                        break;
                }
                if (pos != new Vector3()) Spawn(pos);
            }
        }

        private void Spawn(Vector3 position)
        {
            System.Random rand1 = new System.Random(Utilities.GetRandomHash());
            System.Random rand2 = new System.Random((int)(Utilities.GetRandomHash() * 0.3f));
            if (spawnFromDatabase)
            {
                var items = FireworkSpawner.FindDatabase().Items.ToList();
                int randomIndex2 = rand1.Next(0, items.Count);
                SpawnAndIngniteFirework(position, (FireworkEntityDefinition)items[randomIndex2].Value);
                return;
            }
            int numberOfCakes = FireworkSpawner.KnownFireworks.Cakes.Length;
            int numberOfRockets = FireworkSpawner.KnownFireworks.Rockets.Length;
            int randomIndex = rand1.Next(0, numberOfCakes);
            int randomIndex1 = rand2.Next(0, numberOfRockets);
            SpawnAndIngniteFirework(position, FireworkSpawner.KnownFireworks.Cakes[randomIndex]);
            SpawnAndIngniteFirework(position, FireworkSpawner.KnownFireworks.Rockets[randomIndex1]);
        }

        private bool DoTickCheck()
        {
            if (tick < 10)
            {
                tick++;
                return false;
            }
            tick = 0;
            return true;
        }

        public int Rarity
        {
            set { rarity = value; }
            get { return rarity; }
        }
    }
}