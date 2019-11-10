using AutoMapper;
using ShopChallenge.Repositories.Models;
using ShopChallenge.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ShopChallenge.Services.UserService
{
    //TODO : Add Logging
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public UserService(
            IOptions<AppSettings> appSettings,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            IOptions<PasswordHasherOptions> passwordHasherOptions
            )
        {
            _appSettings = appSettings?.Value;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = new PasswordHasher<UserModel>(passwordHasherOptions);
        }

        public async Task<UserApi> AddUser(UserApi user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userModel = _mapper.Map<UserModel>(user);
                userModel.Password = _passwordHasher.HashPassword(userModel, userModel.Password);
                userModel = await _userRepository.AddUser(userModel).ConfigureAwait(false);
                user = _mapper.Map<UserApi>(userModel);
                _logger.LogInformation($"The user have added {user}");

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error wile adding the user {user}");

                return null;
            }
        }

        public async Task<UserApi> AuthenticateUser(UserApi user)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                var userModel = _mapper.Map<UserModel>(user);
                var dbUser = await _userRepository.GetUser(userModel).ConfigureAwait(false);
                if (dbUser is null)
                {
                    _logger.LogError($"no user {user} found with the same email");

                    return null;
                }
                var passwordResult = _passwordHasher.VerifyHashedPassword(userModel, dbUser.Password, userModel.Password);
                if (passwordResult == PasswordVerificationResult.Failed)
                {
                    _logger.LogWarning($"The user {user} tried to log with wrong passowrd");

                    return null;
                }
                var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, dbUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, dbUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                                  };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.Password = null;
                user.Id = null;
                _logger.LogInformation($"The user {user} have attemped to authenticate");

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while authenticating the user {user}");

                return null;
            }
        }

        public async Task<ShopApi> DislikeShop(UserApi user, string shopId)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (shopId is null)
                    throw new ArgumentNullException(nameof(shopId));

                var shopAoi = new ShopApi { Id = shopId };
                var shopModel = _mapper.Map<ShopModel>(shopAoi);
                var userModel = _mapper.Map<UserModel>(user);
                shopModel = await _userRepository.DislikeShop(userModel, shopModel).ConfigureAwait(false);
                var shopApi = _mapper.Map<ShopApi>(shopModel);
                _logger.LogInformation($"the user {user} disliked the shop {shopAoi}");

                return shopApi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while disliking the shop with id {shopId} by the user {user}");

                return null;
            }
        }

        public async Task<ShopApi> LikeShop(UserApi user, string shopId)
        {
            try
            {
                if (user is null)
                    throw new ArgumentNullException(nameof(user));
                if (shopId is null)
                    throw new ArgumentNullException(nameof(shopId));

                var shopAoi = new ShopApi { Id = shopId };
                var shopModel = _mapper.Map<ShopModel>(shopAoi);
                var userModel = _mapper.Map<UserModel>(user);
                shopModel = await _userRepository.LikeShop(userModel, shopModel).ConfigureAwait(false);
                var shopApi = _mapper.Map<ShopApi>(shopModel);
                _logger.LogInformation($"the user {user} disliked the shop {shopAoi}");

                return shopApi;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while disliking the shop with id {shopId} by the user {user}");

                return null;
            }
        }
    }
}
