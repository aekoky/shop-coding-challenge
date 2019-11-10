using ShopChallenge.Services.ShopService;
using ShopChallenge.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge
{
    public static class ServicesInjector
    {
        public static void InjectServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IShopService, ShopService>();
        }
    }
}
