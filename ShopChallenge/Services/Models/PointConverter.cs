using AutoMapper;
using MongoDB.Driver.GeoJsonObjectModel;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Services.Models
{
    public class PointConverter : IValueConverter<PointApi, GeoJsonPoint<GeoJson2DGeographicCoordinates>>
    {
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Convert(PointApi sourceMember, ResolutionContext context)
        {
            if (sourceMember is null)
                return null;
            var cordinattes = new GeoJson2DGeographicCoordinates(sourceMember.Longitude, sourceMember.Latitude);
            return new GeoJsonPoint<GeoJson2DGeographicCoordinates>(cordinattes);
        }
    }
}
