using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopChallenge.Repositories.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ShopChallenge.Helpers;
using ShopChallenge.Pagination;
using MongoDB.Bson;

namespace ShopChallenge.Repositories.ShopRepository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext _shopDatabase;
        private readonly ILogger<ShopRepository> _logger;

        public ShopRepository(ShopContext shopDatabase, ILogger<ShopRepository> logger)
        {
            _logger = logger;
            _shopDatabase = shopDatabase;
        }
        public async Task<ShopModel> AddShop(ShopModel shop)
        {
            try
            {
                if (shop is null)
                    throw new ArgumentNullException(nameof(shop));
                await _shopDatabase.ShopsCollection.InsertOneAsync(shop).ConfigureAwait(false);
                _logger.LogInformation($"The shop {shop} have been added");

                return shop;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding the shop {shop}");

                return null;
            }
        }

        public async Task<Page<ShopModel>> GetShops(UserModel user, PageModel page)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (page is null)
                    throw new ArgumentNullException(nameof(page));
                ShopModel shopModel;
                var idFilter = Builders<UserModel>.Filter.Eq(nameof(user.Id), user.Id);
                UserModel userModel = await _shopDatabase.UsersCollection.Find(idFilter).SingleOrDefaultAsync().ConfigureAwait(false);
                var excludedShops = new List<ObjectId>();
                if (userModel.LikedShops != null)
                    excludedShops.AddRange(userModel.LikedShops);
                if (userModel.DislikedShops != null)
                    excludedShops.AddRange(userModel.DislikedShops);
                var likedFilter = Builders<ShopModel>.Filter.Nin(nameof(shopModel.Id), excludedShops);
                Page<ShopModel> shops = await _shopDatabase.ShopsCollection.GetPagedAsync(page, likedFilter).ConfigureAwait(false);
                _logger.LogInformation($"All shops had gotten");

                return shops;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting shops");

                return PaginationExtension.GetEmptyPage<ShopModel>();
            }
        }

        public async Task<Page<ShopModel>> GetShopsByDistance(UserModel user, PageModel page)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (page is null)
                    throw new ArgumentNullException(nameof(page));

                var idFilter = Builders<UserModel>.Filter.Eq(nameof(user.Id), user.Id);
                UserModel userModel = await _shopDatabase.UsersCollection.Find(idFilter)
                                                                         .SingleOrDefaultAsync()
                                                                         .ConfigureAwait(false);
                IEnumerable<ShopModel> shops =
                    await _shopDatabase.ShopsCollection.Find(Builders<ShopModel>.Filter.Empty)
                    .ToListAsync().ConfigureAwait(false);
                IEnumerable<ShopModel> shopsByDistance = shops.OrderBy(shop => shop.Coordinates.DistanceTo(userModel.Location));
                _logger.LogInformation($"All shops had gotten ordered by distance by the user {user}");

                return shopsByDistance.GetPage(page);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting shops ordered by distance by the user {user}");

                return PaginationExtension.GetEmptyPage<ShopModel>();
            }
        }

        public async Task<Page<ShopModel>> GetPreferedShops(UserModel user, PageModel page)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (page is null)
                    throw new ArgumentNullException(nameof(page));
                ShopModel shopModel;
                var idFilter = Builders<UserModel>.Filter.Eq(nameof(user.Id), user.Id);
                UserModel userModel = await _shopDatabase.UsersCollection.Find(idFilter).SingleOrDefaultAsync().ConfigureAwait(false);
                Page<ShopModel> shops = null;
                if (userModel.LikedShops == null)
                    shops = await _shopDatabase.ShopsCollection.GetPagedAsync(page).ConfigureAwait(false);
                var likedFilter = Builders<ShopModel>.Filter.In(nameof(shopModel.Id), userModel.LikedShops);
                Page<ShopModel> preferedShops = await _shopDatabase.ShopsCollection.GetPagedAsync(page, likedFilter).ConfigureAwait(false);

                _logger.LogInformation($"All preffered shops had gotten by the user {user}");

                return shops ?? preferedShops;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting prefered shops by the user {user}");

                return PaginationExtension.GetEmptyPage<ShopModel>();
            }
        }
    }
}
