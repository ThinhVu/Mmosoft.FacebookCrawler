using System;

using FbFarm.Sdk.Models.User;
using FbFarm.Sdk;

namespace BoringWorld
{
    public class Farmer
    {
        private FacebookClient _client;
        private UserInfoOption _rule;
        private Action _work;

        public string Name { get; private set; }       
        

        public Farmer(string name, string password)
        {
            Name = name;
            _client = new FacebookClient(name, password);
        }
        public void LearnRule(UserInfoOption rule)
        {
            _rule = rule;
        }
        public void LearnWork(Action work)
        {
            _work = work;
        }
        public void DoWork()
        {
            _work();
        }
        public UserInfo GetProduct(string seed)
        {
            return _client.GetUserInfo(seed, _rule);
        }
    }
}
