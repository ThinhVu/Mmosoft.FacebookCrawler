using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
// 
using FbFarm.Sdk.Models.User;

namespace BoringWorld
{
    public class ProductStore
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        public IMongoCollection<UserInfo> UserInfos { get; private set; }

        public ProductStore(string connectionString = "mongodb://localhost:27017")
        {
            _client = new MongoClient(connectionString);            
            _database = _client.GetDatabase("FbFarm");
            UserInfos = _database.GetCollection<UserInfo>("UserInfos");
        }
    }
}
