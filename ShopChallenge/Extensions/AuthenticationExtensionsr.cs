using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.GeoJsonObjectModel;
using ShopChallenge.Repositories.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace ShopChallenge.Helpers
{
    public static class AuthenticationExtensions
    {
        public static UserApi GetUser(this ControllerBase controll)
        {
            var claimHandler = new ClaimsIdentity(controll.User.Claims);
            var userIDClaim = claimHandler.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.Ordinal));
            if (userIDClaim is null)
                return null;
            return new UserApi { Id = userIDClaim.Value };
        }
    }
}
