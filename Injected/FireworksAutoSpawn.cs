using FireworksMania.Core.Definitions.EntityDefinitions;
using FModApi;
using Main.EnvironmentObserver;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public class FireworksAutoSpawn
    {
        private int m_Rarity = 10;
        private int m_Tick = 0;

        public void SpawnAndIngniteFirework(Vector3 position, string id)
        {
            // TODO: Fix this and correct the values.
            var firework = (FireworkEntityDefinition)FireworkSpawner.GetEntityDefinition(id);
            IgniteCoroutine(FireworkSpawner.SpawnFirework(firework, position));

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
                int value = random.Next(1, m_Rarity);
                if (value == m_Rarity / 2) SpawnRandom();
            }
        }

        private void SpawnRandom()
        {
            if (ModSceneManager.IsPlayableMapLoaded())
            {
                System.Random rand = new System.Random();
                Vector3 pos = new Vector3();
                switch (ModSceneManager.GetLoadedMap())
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
            System.Random rand1 = new System.Random();
            int numberOfCakes = FireworkSpawner.KnownFireworks.Cakes.Length;
            int numberOfRockets = FireworkSpawner.KnownFireworks.Rockets.Length;
            int randomIndex = rand1.Next(0, numberOfCakes);
            int randomIndex1 = rand1.Next(0, numberOfRockets);
            SpawnAndIngniteFirework(position, FireworkSpawner.KnownFireworks.Cakes[randomIndex]);
            SpawnAndIngniteFirework(position, FireworkSpawner.KnownFireworks.Rockets[randomIndex1]);
        }

        private bool DoTickCheck()
        {
            if (m_Tick < 10)
            {
                m_Tick++;
                return false;
            }
            m_Tick = 0;
            return true;
        }

        public int Rarity
        {
            set { m_Rarity = value; }
            get { return m_Rarity; }
        }
    }
}
