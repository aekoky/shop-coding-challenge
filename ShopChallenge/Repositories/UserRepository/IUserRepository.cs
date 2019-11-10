using ShopChallenge.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopChallenge.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<UserModel> AddUser(UserModel user);
        Task<UserModel> GetUser(UserModel user);
        Task<ShopModel> LikeShop(UserModel user, ShopModel shop);
        Task<ShopModel> DislikeShop(UserModel user, ShopModel shop);
    }
}
