using FireworksMania.ScriptableObjects.EntityDefinitions;
using FModApi;
using Main.FModApi;
using System.Threading.Tasks;
using UnityEngine;

namespace Main
{
    public class SilvesterSimulation
    {
        private int m_Rarity = 25;
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
                int seed = Utils.GetRandomHash();
                System.Random rand = new System.Random(seed);
                int val = rand.Next(1, m_Rarity);
                if (val == m_Rarity / 2) SpawnRandom();
            }
        }

        private void SpawnRandom()
        {
            if (ModSceneManager.IsPlayableMapLoaded())
            {
                int seed = Utils.GetRandomHash() + (int)Time.deltaTime * 100;
                System.Random rand = new System.Random(seed);
                Vector3 pos = new Vector3();
                switch (ModSceneManager.GetActiveSceneIndex())
                {
                    case 4:
                        pos = Utils.GetRandomIntVector(rand, -100, 100, 1);
                        break;
                    case 6:
                        pos = Utils.GetRandomIntVector(rand, -100, 100, 1);
                        break;
                }
                if (pos != new Vector3()) Spawn(pos);
            }
        }

        private void Spawn(Vector3 position)
        {
            int seed1 = Utils.GetRandomHash() + Time.frameCount;
            System.Random rand1 = new System.Random(seed1);
            int numberOfCakes = FireworkSpawner.KnownFireworks.Cakes.Length;
            int randomIndex = rand1.Next(0, numberOfCakes);
            SpawnAndIngniteFirework(position, FireworkSpawner.KnownFireworks.Cakes[randomIndex]);
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
