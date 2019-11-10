using ShopChallenge.Repositories.ShopRepository;
using ShopChallenge.Repositories.UserRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge
{
    public static class RepositoriesInjector
    {
        public static void InjectRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IShopRepository, ShopRepository>();
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
        }

    }
}
