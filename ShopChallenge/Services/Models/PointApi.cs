namespace ShopChallenge.Repositories.Models
{
    public class PointApi
    {
        public double Longitude { get; set; }
     
        public double Latitude { get; set; }

        public override string ToString()
        {
            return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}";
        }
    }
}