using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Extensions;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [Authorize]
    [Route("api/v1/wishlists")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListController(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        // GET api/v1/wishlists
        [HttpGet]
        public async Task<IActionResult> GetUserWishLists()
        {
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty)
                return BadRequest();
            var wishList = await _wishListRepository.GetUserWishListsAsync(userId);
            return Ok(wishList);
        }

        // GET api/v1/wishlists/{wishListName}/books
        [HttpGet("{wishListName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWishListByName([FromRoute] string wishListName)
        {
            var wishList = await _wishListRepository.GetWishListByNameAsync(wishListName);
            return wishList is null ? NotFound() : Ok(wishList);
        }

        // POST api/v1/wishlists
        [HttpPost]
        public async Task<IActionResult> CreateWishList([FromBody] CreateWishListRequest request)
        {
            var userId = HttpContext.GetUserId();

            var wishList = await _wishListRepository.CreateWishListAsync(request.WishListName, userId);

            if (wishList == null)
                return BadRequest();

            return Ok(wishList);
        }

        // DELETE api/v1/wishlists/{wishlistName}
        [HttpDelete("{wishListName}")]
        public async Task<IActionResult> DeleteWishList([FromRoute] string wishListName)
        {
            bool deleted = await _wishListRepository.DeleteWishListAsync(wishListName);
            return deleted ? NoContent() : BadRequest();
        }

        // POST api/v1/wishlists/{wishListName}/books
        [HttpPost("{wishListName}/books")]
        public async Task<IActionResult> AddBookToWishList(
            [FromRoute] string wishListName, [FromBody] AddBookToWishListRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// Removes a book from a wishlist, either permanently or into a shopping cart.
        /// </summary>
        /// <example>DELETE api/v1/wishlists/{wishListName}/books/{bookId}</example>
        /// <param name="wishListName">Name of the wishlist to modify</param>
        /// <param name="bookId">Id of the book to remove</param>
        /// <param name="request">Additional request parameters</param>
        /// <seealso cref="RemoveBookFromWishListRequest"/>
        /// <returns></returns>
        [HttpDelete("{wishListName}/books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromWishList(
            [FromRoute] string wishListName, [FromRoute] Guid bookId, [FromBody] RemoveBookFromWishListRequest request)
        {
            return Ok();
        }
    }

}

