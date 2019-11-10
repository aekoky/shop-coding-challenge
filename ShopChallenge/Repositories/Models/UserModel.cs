using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace ShopChallenge.Repositories.Models
{
    public class UserModel
    {
        private string email;

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        [BsonElement("email")]
        public string Email { get => email?.ToUpperInvariant(); set => email = value; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        [BsonElement("likedShops")]
        public ICollection<ObjectId> LikedShops { get; set; } = new List<ObjectId>();

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("dislikedShops")]
        public ICollection<ObjectId> DislikedShops { get; set; } = new List<ObjectId>();

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Email)}: {Email}, {nameof(Location)}: {Location}";
        }
    }
}
