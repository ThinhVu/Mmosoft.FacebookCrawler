using System;
using System.Collections.Generic;
using System.IO;

using FbFarm.Sdk;
using FbFarm.Sdk.Exceptions;
using FbFarm.Sdk.Models.User;

namespace BoringWorld
{
    // Your clone established a farm that produces various kinds of weird food in the boring world.
    // Your clone hired a woman to manage the farm, but she doesn’t know what to do, so she always asks you for guidance.
    // Your clone is the boss of this farm and has the responsibility to launch or pause its operations.
    // Remember that:
    // You do not live in the boring world—this is just your clone.
    public static class YourClone
    {
        public static Farm EstablishFarm()
        {            
            var farmRule = new UserInfoOption();
            // facebook info options
            farmRule.FbInfoOption = new FacebookInfoOption();
            farmRule.FbInfoOption.IncludeUserId = true;
            farmRule.FbInfoOption.IncludeAvatarUrl = true;
            farmRule.FbInfoOption.IncludeFbUrl = true;
            farmRule.FbInfoOption.IncludeUserDisplayName = true;
            farmRule.FbInfoOption.IncludeFbFriends = true;
            // user real info
            farmRule.IncludeAddressInfo = true;
            farmRule.IncludeBasicInfo = true;
            farmRule.IncludeContactInfo = true;
            farmRule.IncludeEduInfo = false;
            farmRule.IncludeWorkInfo = false;
            farmRule.IncludeRelationshipInfo = false;

            return new Farm(farmRule);
        }
        // A gate to the boring world
        public static void Main()
        {
            var yourFarm = YourClone.EstablishFarm();
            var farmerInBoringWorlds = YoungManager.GetFarmerInfo();
            foreach (var farmer in farmerInBoringWorlds)
                yourFarm.Hire(farmer);

            while (true)
            {
                YoungManager.Say("Excuse me! Should we start the harvest now? [Yes/No]");
                string yourAnswer = YourClone.GiveHerAnAnswer();
                if (yourAnswer.Equals("Yes"))
                {
                    yourFarm.StartHarvest();
                    break;
                }
                else
                {
                    YoungManager.Say("So I'll wait. Hmm......");
                }
            }
        }

        public static string GiveHerAnAnswer()
        {
            return Console.ReadLine();
        }
    }

    public static class YoungManager
    {        
        public static Farmer[] GetFarmerInfo()
        {            
            if (!File.Exists("Data\\Farmers.txt"))
                throw new Exception("Data\\Farmers.txt does not exist.");

            var fms = new List<Farmer>();
            string[] farmerInfos = File.ReadAllLines("Data\\Farmers.txt");
            for (int i = 0; i < farmerInfos.Length; i++)
            {
                try
                {
                    // ignore
                    if (farmerInfos[i].StartsWith("//") || farmerInfos[i].Trim().Length == 0)
                        continue;

                    // process famer infor
                    var farmerIdentity = farmerInfos[i].Split(',');
                    if (farmerIdentity.Length != 2)
                        continue;

                    // validate farmer profile
                    gSecretary.WriteLine("Validate farmer profile: " + farmerIdentity[0]);
                    var farmer = new Farmer(farmerIdentity[0], farmerIdentity[1]);
                    fms.Add(farmer);
                }
                catch (UnAuthorizedException ex)
                {
                    Console.WriteLine("This is not farmer because: " + ex.Message);
                }
            }
            return fms.ToArray();
        }
        public static void Say(string msg)
        {
            Console.WriteLine(msg);            
        }
    }
}
