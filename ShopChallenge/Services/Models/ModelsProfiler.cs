using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Services.Models
{
    public class ModelsProfiler : Profile
    {
        public ModelsProfiler()
        {
            this.CreateMap<UserModel, UserApi>()
                                .ForMember(x => x.Coordinates, opt => opt.Ignore())
                                .ForMember(x => x.Token, opt => opt.Ignore())
                                .ReverseMap()
                                .ForMember(x => x.DislikedShops, opt => opt.Ignore())
                                .ForMember(x => x.LikedShops, opt => opt.Ignore())
                                .ForMember(x => x.Location, opt => opt.ConvertUsing(new PointConverter(), src => src.Coordinates));

            this.CreateMap<ShopModel, ShopApi>().ReverseMap()
                                .ForMember(x => x.City, opt => opt.Ignore())
                                .ForMember(x => x.Coordinates, opt => opt.Ignore())
                                .ForMember(x => x.Email, opt => opt.Ignore());

            this.CreateMap<ObjectId, string>().ConvertUsing(o => o.ToString());

            this.CreateMap<string, ObjectId>().ConvertUsing(s => ObjectId.Parse(s));
        }
    }
}
