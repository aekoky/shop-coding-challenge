using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShopChallenge.Repositories.Models
{
    public class ShopApi
    {
        public string Id { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }
    }
}
