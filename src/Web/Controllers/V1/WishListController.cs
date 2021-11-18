using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Errors;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [Authorize]
    [Route("api/v1/wishlists")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public WishListController(
            IWishListRepository wishListRepository,
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        // GET api/v1/wishlists
        [HttpGet]
        public async Task<IActionResult> GetUserWishLists()
        {
            var userId = HttpContext.GetUserId();
            if (userId == Guid.Empty) return BadRequest();

            var wishList = await _wishListRepository.GetUserWishListsAsync(userId);
            var mapping = _mapper.Map<List<WishList>, List<WishListResponse>>(wishList);
            return Ok(mapping.ToListedResponse());
        }

        // GET api/v1/wishlists/{wishListName}/books
        [HttpGet("{wishListName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWishListByName([FromRoute] string wishListName)
        {
            var wishList = await _wishListRepository.GetWishListByNameAsync(wishListName);
            if (wishList is null) return NotFound(new {Error = $"Wishlist {wishListName} does not exist."});

            var mapping = _mapper.Map<WishList, WishListResponse>(wishList);

            return Ok(mapping.ToSingleResponse());
        }

        // POST api/v1/wishlists
        [HttpPost]
        public async Task<IActionResult> CreateWishList([FromBody] CreateWishListRequest request)
        {
            var userId = HttpContext.GetUserId();
            (string wishListName, string description, _) = request;

            bool exists = await _wishListRepository.WishListExists(wishListName);
            if (exists) return BadRequest(new {Error = $"Wishlist {wishListName} already exists."});

            bool exceeds = await _wishListRepository.UserExceedsWishListsLimit(userId);
            if (exceeds) return BadRequest(new { Error = "You reached the maximum number of wishlists." });

            var wishlist = new WishList { Name = wishListName, Description = description, UserId = userId };
            bool success = await _wishListRepository.CreateWishListAsync(wishlist);
            if (!success) return BadRequest(new { Error = "Unable to create the wishlist."});

            return CreatedAtAction(nameof(GetWishListByName), new {wishListName = wishlist.Name}, wishlist);
        }

        // DELETE api/v1/wishlists/{wishlistName}
        [HttpDelete("{wishListName}")]
        public async Task<IActionResult> DeleteWishList([FromRoute] string wishListName)
        {
            var userId = HttpContext.GetUserId();

            bool exists = await _wishListRepository.WishListExists(wishListName);
            if (!exists) return NotFound(new {Error = $"Wishlist {wishListName} does not exist."});

            bool isOwner = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!isOwner) return BadRequest(new { Error = "You do not own this wishlist."});

            bool deleted = await _wishListRepository.DeleteWishListAsync(wishListName);
            if (!deleted) return BadRequest(new {Error = "Unable to delete the wishlist."});

            return NoContent();
        }

        // POST api/v1/wishlists/{wishListName}/books
        [HttpPost("{wishListName}/books")]
        public async Task<IActionResult> AddBookToWishList(
            [FromRoute] string wishListName, [FromBody] AddBookToWishListRequest request)
        {
            var userId = HttpContext.GetUserId();

            bool exists = await _wishListRepository.WishListExists(wishListName);
            if (!exists)
                return NotFound(new { Error = $"Wishlist {wishListName} does not exist." });

            bool isOwner = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!isOwner)
                return BadRequest(new { Error = "You do not own this wishlist." });

            bool bookExists = await _bookRepository.BookExistsAsync(request.BookId);
            if (!bookExists)
                return NotFound(new { Error = $"Book {request.BookId} does not exist." });

            bool added = await _wishListRepository.AddBookToWishListAsync(wishListName, request.BookId);
            if (!added)
                return BadRequest(new {Error = "Unable to add book to the wishlist"});

            return NoContent();
        }

        /// <summary>
        /// Removes book from wishlist, either permanently or adds it to the shopping cart.
        /// </summary>
        /// <remarks>This can only be done by the logged in user</remarks>
        /// <param name="wishListName">Name of the wishlist to modify</param>
        /// <param name="bookId">Id of the book to remove</param>
        /// <param name="cartId">Adds the removed book to the cart (Optional)</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid wishlist/book supplied</response>
        /// <response code="401">User does not own resource</response>
        /// <response code="404">Wishlist or book not found</response>
        [HttpDelete("{wishListName}/books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromWishList(
            [FromRoute] string wishListName, [FromRoute] Guid bookId, [FromQuery] Guid cartId)
        {
            var userId = HttpContext.GetUserId();
            bool ownsWishList = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!ownsWishList) return Unauthorized(new UserIsNotOwner(nameof(WishList)));

            // Todo: Check if user owns the target cart.

            var result = await _wishListRepository.RemoveBookFromWishListAsync(wishListName, bookId, cartId);
            return result.Match(Ok, error => error.GetResultFromError());
        }
    }

}

