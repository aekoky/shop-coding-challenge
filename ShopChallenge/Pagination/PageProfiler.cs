using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Pagination
{
    public class PageProfiler : Profile
    {
        public PageProfiler()
        {
            CreateMap(typeof(Page<>), typeof(Page<>));
        }
    }
}
