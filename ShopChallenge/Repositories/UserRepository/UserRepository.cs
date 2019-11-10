using ShopChallenge.Repositories.Models;
using ShopChallenge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Bson;
using ShopChallenge.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ShopChallenge.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopContext _shopDatabase;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ShopContext shopDatabase, ILogger<UserRepository> logger)
        {
            _shopDatabase = shopDatabase;
            _logger = logger;
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                var emailFilter = Builders<UserModel>.Filter.Eq(usr => usr.Email, user.Email);
                var dbUser = await _shopDatabase.UsersCollection.Find(emailFilter).SingleOrDefaultAsync().ConfigureAwait(false);
                if (dbUser != null)
                {
                    _logger.LogInformation($"The user already exists {user}");
                    return null;
                }
                await _shopDatabase.UsersCollection.InsertOneAsync(user).ConfigureAwait(false);
                _logger.LogInformation($"The user added {user}");

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while adding the user {user}");

                return null;
            }
        }

        public async Task<UserModel> GetUser(UserModel user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                var emailFilter = Builders<UserModel>.Filter.Eq(usr => usr.Email, user.Email);
                var dbUser = await _shopDatabase.UsersCollection.Find(emailFilter).SingleOrDefaultAsync().ConfigureAwait(false);
                _logger.LogInformation($"got the user {user} sucssuflly");

                return dbUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting the user {user}");

                return null;
            }
        }

        public async Task<ShopModel> DislikeShop(UserModel user, ShopModel shop)
        {
            try
            {
                if (shop is null)
                    throw new ArgumentNullException(nameof(shop));
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                var idFilter = Builders<UserModel>.Filter.Eq(nameof(user.Id), user.Id);
                var updateCommand = Builders<UserModel>.Update.AddToSet(nameof(user.DislikedShops), shop.Id);
                await _shopDatabase.UsersCollection.UpdateOneAsync(idFilter, updateCommand).ConfigureAwait(false);
                _logger.LogInformation($"the shop {shop} have disliked by the user {user}");

                return shop;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while disliking the shop {shop} by the user {user}");

                return null;
            }
        }

        public async Task<ShopModel> LikeShop(UserModel user, ShopModel shop)
        {
            try
            {
                if (shop is null)
                    throw new ArgumentNullException(nameof(shop));
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                var idFilter = Builders<UserModel>.Filter.Eq(nameof(user.Id), user.Id);
                var updateCommand = Builders<UserModel>.Update.AddToSet(nameof(user.LikedShops), shop.Id);
                await _shopDatabase.UsersCollection.UpdateOneAsync(idFilter, updateCommand).ConfigureAwait(false);
                _logger.LogInformation($"the shop {shop} have liked by the user {user}");

                return shop;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while liking the shop {shop} by the user {user}");

                return null;
            }
        }
    }
}
