using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BoringWorld
{
    public class SeedStore
    {
        private string _seedPath = "Data\\Seeds.txt";
        private string _oldSeedPath = "Data\\OldSeeds.txt";
        private HashSet<string> _oldSeeds;
        private Queue<string> _seeds;
        private object _store;

        public SeedStore()
        {
            _store = new object();

            // import old seed
            var oldSeeds = File.ReadAllLines(_oldSeedPath);
            _oldSeeds = new HashSet<string>();
            foreach (var oldSeed in oldSeeds)
                _oldSeeds.Add(oldSeed);

            // import seed
            _seeds = new Queue<string>();
            var seeds = File.ReadAllLines(_seedPath);
            foreach (var seed in seeds)
            {
                if (!_oldSeeds.Contains(seed))
                    _seeds.Enqueue(seed);
            }
        }

        public bool Available()
        {
            lock (_store)
            {
                return _seeds.Count > 0;
            }
        }
        public string GetSeed()
        {
            string seed = null;
            lock (_store)
            {
                if (_seeds.Count > 0)
                {
                    seed = _seeds.Dequeue();
                    // Store it in old seed
                    _oldSeeds.Add(seed);
                }
            }
            return seed;
        }
        public void SetSeeds(List<string> seeds)
        {
            lock (_store)
            {
                for (int i = 0; i < seeds.Count; i++)
                {
                    // already process seed will not be added anymore
                    if (!_oldSeeds.Contains(seeds[i]))
                        _seeds.Enqueue(seeds[i]);
                }
            }
        }

        public void SaveToDisk()
        {
            File.WriteAllLines(_oldSeedPath, _oldSeeds.ToArray());
            File.WriteAllLines(_seedPath, _seeds.ToArray());            
        }
    }
}