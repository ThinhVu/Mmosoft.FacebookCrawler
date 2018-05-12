using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FbFarm.Sdk.Models.User;
using System.IO;
using System.Threading;

namespace BoringWorld
{

    public class Farm
    {       
        private List<Task> _tasks;
        private List<Farmer> _farmers;
        private UserInfoOption _farmRule;
        private CancellationTokenSource _stopSignal;
        // In-memory db
        private SeedStore _seedStore;
        // MongoDb
        private ProductStore _productStore;

        public Farm(UserInfoOption farmRule)
        {
            if (!Directory.Exists("Data"))
                Directory.CreateDirectory("Data");
            _farmRule = farmRule;
            _seedStore = new SeedStore();
            _productStore = new ProductStore();
            _farmers = new List<Farmer>();
            _stopSignal = new CancellationTokenSource();
        }

        public void Hire(Farmer farmer)
        {
            // hire he/she if his/her profile is valid
            _farmers.Add(farmer);
            // basic training about seed store and product store
            // farmer will know what to do with these place
            farmer.LearnRule(_farmRule);
            farmer.LearnWork(() =>
            {
                while (true)
                {
                    if (_stopSignal.IsCancellationRequested)
                        break;

                    try
                    {
                        if (_seedStore.Available())
                        {
                            var seed = _seedStore.GetSeed();
                            var product = farmer.GetProduct(seed);
                            _productStore.UserInfos.InsertOne(product);
                            _seedStore.SetSeeds(product.FBInfo.FbFriends);
                        }
                        else
                        {
                            Console.WriteLine(farmer.Name + " is going to sleep.");
                            Task.Delay(60000); // 1 minutes
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(farmer.Name + " did something wrong. This is what he/she did : " + ex.Message);
                    }
                }
            });
        }
        public void StartHarvest()
        {            
            if (_farmers.Count == 0)
            {
                Console.WriteLine("But our farm have no farmers");
                return;
            }

            // start harvest
            Console.WriteLine("Every body, start harvest");
            var cancelSignal = _stopSignal.Token;
            _tasks = new List<Task>(_farmers.Count);

            for (int i = 0; i < _farmers.Count; i++)
            {
                int ii = i;
                Console.WriteLine("Farmer " + _farmers[ii].Name + " start working.");
                _tasks.Add(Task.Factory.StartNew(()=>_farmers[ii].DoWork()));
            }

            ReportDaily();
        }

        private void ReportDaily()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (true)
                    {
                        if (_stopSignal.Token.IsCancellationRequested)
                            break;

                        for (int i = 0; i < _tasks.Count; i++)
                        {
                            Console.WriteLine("Farmer " + _farmers[i].Name + " currently " + _tasks[i].Status);
                        }

                        Thread.Sleep(30000); // delay 1m
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }, _stopSignal.Token);
        }
    }
}
