using ShopChallenge.Pagination;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Repositories.ShopRepository
{
    public interface IShopRepository
    {
        Task<ShopModel> AddShop(ShopModel shop);

        Task<Page<ShopModel>> GetShops(UserModel user, PageModel page);

        Task<Page<ShopModel>> GetShopsByDistance(UserModel user, PageModel page);

        Task<Page<ShopModel>> GetPreferedShops(UserModel user, PageModel page);
    }
}
