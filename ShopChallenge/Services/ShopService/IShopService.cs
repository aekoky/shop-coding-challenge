using ShopChallenge.Pagination;
using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Services.ShopService
{
    public interface IShopService
    {
        Task<ShopApi> AddShop(ShopApi shop);

        Task<Page<ShopApi>> GetShops(UserApi user, int page, int pageSize);

        Task<Page<ShopApi>> GetShopsByDistance(UserApi user, int page, int pageSize);

        Task<Page<ShopApi>> GetPreferedShops(UserApi user, int page, int pageSize);
    }
}
