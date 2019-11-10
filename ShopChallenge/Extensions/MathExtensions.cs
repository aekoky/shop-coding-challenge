using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopChallenge.Helpers
{
    public static class MathExtensions
    {
        // The radius of the earth in Km.
        private const double R = 6371E3;

        public static double DistanceTo(this GeoJsonPoint<GeoJson2DGeographicCoordinates> startPoint, GeoJsonPoint<GeoJson2DGeographicCoordinates> destination)
        {
            if (startPoint?.Coordinates is null || destination?.Coordinates is null)
                return double.NaN;

            double startLatitude = startPoint.Coordinates.Latitude;
            double startLongitude = startPoint.Coordinates.Longitude;
            double destinationLatitude = destination.Coordinates.Latitude;
            double destinationLongitude = destination.Coordinates.Longitude;

            double fromPointRadianLatitude = ConvertToRadians(startLatitude); ;
            double toPointRadianLatitude = ConvertToRadians(destinationLatitude);

            double df = ConvertToRadians(startLatitude - destinationLatitude);
            double dl = ConvertToRadians(startLongitude - destinationLongitude);

            double a = Math.Sin(df / 2) * Math.Sin(df / 2) +
            Math.Cos(fromPointRadianLatitude) * Math.Cos(toPointRadianLatitude) *
            Math.Sin(dl / 2) * Math.Sin(dl / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
