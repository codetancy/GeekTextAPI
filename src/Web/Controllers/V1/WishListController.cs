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
            (string wishListName, string description) = request;

            bool exists = await _wishListRepository.WishListExists(wishListName);
            if (exists) return BadRequest(new {Error = $"Wishlist {wishListName} already exists."});

            bool exceeds = await _wishListRepository.UserExceedsWishListsLimit(userId);
            if (exceeds) return BadRequest(new { Error = "You reached the maximum number of wishlists." });

            var wishlist = new WishList { Name = wishListName, Description = description, UserId = userId };
            bool success = await _wishListRepository.CreateWishListAsync(wishlist);
            if (!success) return BadRequest(new { Error = "Unable to create the wishlist."});

            return CreatedAtAction(nameof(GetWishListByName), new {wishListName = wishlist.Name}, wishlist);
        }

        /// <summary>
        /// Deletes a wishlist
        /// </summary>
        /// <param name="wishListName">Wishlist to delete</param>
        /// <response code="200">Wishlist deleted successfully</response>
        /// <response code="400">Invalid wishlist supplied</response>
        /// <response code="403">User does not own wishlist</response>
        [HttpDelete("{wishListName}")]
        public async Task<IActionResult> DeleteWishList([FromRoute] string wishListName)
        {
            var userId = HttpContext.GetUserId();
            bool userOwnsWishList = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!userOwnsWishList) return Forbid();

            var result = await _wishListRepository.DeleteWishListAsync(wishListName);
            return result.Match(Ok, error => error.GetResultFromError());
        }

        /// <summary>
        /// Adds book to wishlist.
        /// </summary>
        /// <remarks>Can only be done by logged in user</remarks>
        /// <param name="wishListName">Target wishlist</param>
        /// <param name="bookId">Book to add</param>
        /// <response code="200">Book added to wishlist</response>
        /// <response code="400">Invalid wishlist/book supplied</response>
        /// <response code="403">User does not own wishlist</response>
        [HttpPost("{wishListName}/books")]
        public async Task<IActionResult> AddBookToWishList(
            [FromRoute] string wishListName, [FromBody] Guid bookId)
        {
            var userId = HttpContext.GetUserId();
            bool userOwnsWishList = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!userOwnsWishList) return Forbid();

            var result = await _wishListRepository.AddBookToWishListAsync(wishListName, bookId);
            return result.Match(Ok, error => error.GetResultFromError());
        }

        /// <summary>
        /// Removes book from wishlist, either permanently or adds it to the shopping cart.
        /// </summary>
        /// <remarks>Can only be done by logged in user</remarks>
        /// <param name="wishListName">Target wishlist</param>
        /// <param name="bookId">Book to remove</param>
        /// <param name="cartId">Cart to add removed book (Optional)</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid wishlist/book supplied</response>
        /// <response code="403">User does not own wishlist</response>
        [HttpDelete("{wishListName}/books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromWishList(
            [FromRoute] string wishListName, [FromRoute] Guid bookId, [FromQuery] Guid cartId)
        {
            var userId = HttpContext.GetUserId();
            bool userOwnsWishList = await _wishListRepository.UserOwnsWishList(wishListName, userId);
            if (!userOwnsWishList) return Forbid();

            // Todo: Check if user owns the target cart.

            var result = await _wishListRepository.RemoveBookFromWishListAsync(wishListName, bookId, cartId);
            return result.Match(Ok, error => error.GetResultFromError());
        }
    }

}

