using System.Threading.Tasks;
using ShopChallenge.Repositories.Models;
using ShopChallenge.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShopChallenge.Helpers;

namespace ShopChallenge.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private UserApi _user;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public new UserApi User => _user ?? (_user = this.GetUser());

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateUser([FromBody]UserApi user)
        {
            UserApi userInfo = await _userService.AuthenticateUser(user).ConfigureAwait(false);
            if (userInfo is null)
                return BadRequest(user);

            return Ok(userInfo);
        }

        [AllowAnonymous]
        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody]UserApi user)
        {
            var userApi = await _userService.AddUser(user).ConfigureAwait(false);
            if (userApi == null)
                return BadRequest(user);

            return Ok(userApi);
        }

        [HttpPost("like/{shopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LikeShop([FromRoute] string shopId)
        {
            var shopApi = await _userService.LikeShop(User, shopId).ConfigureAwait(false);
            if (shopApi == null)
                return BadRequest();

            return Ok(shopApi);
        }

        [HttpPost("dislike/{shopId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DislikeShop([FromRoute] string shopId)
        {
            var shopApi = await _userService.DislikeShop(User, shopId).ConfigureAwait(false);
            if (shopApi == null)
                return BadRequest();

            return Ok(shopApi);
        }
    }
}
