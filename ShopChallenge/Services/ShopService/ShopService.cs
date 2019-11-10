using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ShopChallenge.Helpers;
using ShopChallenge.Pagination;
using ShopChallenge.Repositories.Models;
using ShopChallenge.Repositories.ShopRepository;

namespace ShopChallenge.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ShopService> _logger;

        public ShopService(IShopRepository shopRepository, IMapper mapper, ILogger<ShopService> logger)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ShopApi> AddShop(ShopApi shop)
        {
            try
            {
                if (shop is null)
                    throw new ArgumentNullException(nameof(shop));

                var shopModel = _mapper.Map<ShopModel>(shop);
                shopModel = await _shopRepository.AddShop(shopModel);
                var outputShopModel = _mapper.Map<ShopApi>(shopModel);
                _logger.LogInformation($"The shop {shop} had been added");
                
                return outputShopModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding the shop {shop}");

                return null;
            }
        }

        public async Task<Page<ShopApi>> GetPreferedShops(UserApi user, int page, int pageSize)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                
                var userModel = _mapper.Map<UserModel>(user);
                var pagingModel = new PageModel(page, pageSize);
                Page<ShopModel> shopList = await _shopRepository.GetPreferedShops(userModel, pagingModel);
                var preferedShops = _mapper.Map<Page<ShopApi>>(shopList);
                _logger.LogInformation($"The prefered shops had been gotten by the user {user}");

                return preferedShops;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting prefered shops for the user {user}");

                return PaginationExtension.GetEmptyPage<ShopApi>();
            }
        }

        public async Task<Page<ShopApi>> GetShops(UserApi user, int page, int pageSize)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userModel = _mapper.Map<UserModel>(user);
                var pagingModel = new PageModel(page, pageSize);
                Page<ShopModel> shopList = await _shopRepository.GetShops(userModel, pagingModel);
                var shops = _mapper.Map<Page<ShopApi>>(shopList);
                _logger.LogInformation($"The shops had been gotten by the user {user}");

                return shops;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting shops for the user {user}");

                return PaginationExtension.GetEmptyPage<ShopApi>();
            }
        }

        public async Task<Page<ShopApi>> GetShopsByDistance(UserApi user, int page, int pageSize)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userModel = _mapper.Map<UserModel>(user);
                var pagingModel = new PageModel(page, pageSize);
                Page<ShopModel> shopList = await _shopRepository.GetShopsByDistance(userModel, pagingModel);
                var shops = _mapper.Map<Page<ShopApi>>(shopList);
                _logger.LogInformation($"The shops by distance had been gotten by the user {user}");

                return shops;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error getting shops by distance for the user {user}");

                return PaginationExtension.GetEmptyPage<ShopApi>();
            }
        }
    }
}
