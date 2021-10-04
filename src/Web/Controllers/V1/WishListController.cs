using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListController(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        //GET api/v1/wishlist
        [HttpGet]

        public async Task<IActionResult> GetWishList()
        {

            var wishList = await _wishListRepository.GetWishListAsync();
            return Ok(wishList);
        }

        //GET api/v1/wishlist/{wishListName}/books
        [HttpGet("{wishListName:string}")]

        public async Task<IActionResult> GetWishListByName([FromRoute] string wishListName)
        {
            var wishList = await _wishListRepository.GetWishListByNameAsync(wishListName);

            if (wishList is null)
                return BadRequest();

            return Ok(wishList);
        }
    }

}

