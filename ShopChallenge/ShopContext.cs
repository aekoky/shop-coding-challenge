using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ShopChallenge.Repositories.Models;

namespace ShopChallenge
{
    public class ShopContext : DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public IMongoCollection<ShopModel> ShopsCollection => _mongoDatabase.GetCollection<ShopModel>("shops");
        public IMongoCollection<UserModel> UsersCollection => _mongoDatabase.GetCollection<UserModel>("users");

        public ShopContext(MongoClient mongoClient, AppSettings appSettings)
        {
            if (mongoClient != null && appSettings != null)
                _mongoDatabase = mongoClient.GetDatabase(appSettings.DatabaseName);
        }

    }
}

