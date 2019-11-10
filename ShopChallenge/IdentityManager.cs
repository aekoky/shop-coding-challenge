using ShopChallenge.Repositories.Models;
using ShopChallenge.Services.UserService;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ShopChallenge
{
    public class IdentityManager : IUserValidator<UserApi>
    {
        readonly IUserService _userService;
        public IdentityManager(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IdentityResult> ValidateAsync(UserManager<UserApi> manager, UserApi user)
        {
            UserApi authenticatedUser = await _userService.AuthenticateUser(user).ConfigureAwait(false);
            if (authenticatedUser is null)
                return await manager.AccessFailedAsync(user).ConfigureAwait(false);
            return await manager.AddClaimAsync(authenticatedUser, new System.Security.Claims.Claim("identity", authenticatedUser.Token)).ConfigureAwait(false);
        }
    }
}
