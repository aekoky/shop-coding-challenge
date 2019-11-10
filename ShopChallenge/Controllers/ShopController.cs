using System.Threading.Tasks;
using ShopChallenge.Repositories.Models;
using ShopChallenge.Services.ShopService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopChallenge.Helpers;

namespace ShopChallenge.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private UserApi _user;
        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }
        public new UserApi User => _user ?? (_user = this.GetUser());

        [HttpGet("shops")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShops(int page, int pageSize)
        {
            var result = await _shopService.GetShops(User, page, pageSize).ConfigureAwait(false);
            if (result.DataCount == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("shopsByDistance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetShopsByDistance(int page, int pageSize)
        {
            var result = await _shopService.GetShopsByDistance(User, page, pageSize).ConfigureAwait(false);
            if (result.DataCount == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("shops/liked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPreferedShops(int page, int pageSize)
        {
            var result = await _shopService.GetPreferedShops(User, page, pageSize).ConfigureAwait(false);
            if (result.DataCount == 0)
                return NoContent();

            return Ok(result);
        }

        [HttpPost("shops")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddShop([FromBody]ShopApi shop)
        {
            var addedShop = await _shopService.AddShop(shop).ConfigureAwait(false);

            return Ok(addedShop);
        }

    }
}
