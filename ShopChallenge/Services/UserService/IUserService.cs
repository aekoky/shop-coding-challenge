using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Services.UserService
{
    public interface IUserService
    {
        Task<UserApi> AddUser(UserApi user);
        Task<ShopApi> LikeShop(UserApi user, string shopId);
        Task<ShopApi> DislikeShop(UserApi user, string shopId);
        Task<UserApi> AuthenticateUser(UserApi user);
    }
}
