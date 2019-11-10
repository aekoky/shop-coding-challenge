using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ShopChallenge.Repositories.Models
{
    public class ShopModel
    {
        public ObjectId Id { get; set; }

        [BsonElement("picture")]
        public string Picture { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("location")]
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Coordinates { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Email)}: {Email}, {nameof(Name)}: {Name}";
        }
    }
}
