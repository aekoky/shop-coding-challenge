using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ShopChallenge.Repositories.Models
{
    public class PointModel : GeoJson2DGeographicCoordinates
    {
        public PointModel(double longitude, double latitude) : base(longitude, latitude)
        {

        }
        public PointModel() : this(double.NaN, double.NaN)
        {

        }

        [BsonElement("type")]
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}";
        }
    }
}