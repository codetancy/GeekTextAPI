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

        public WishListController(
            IWishListRepository wishListRepository,
            IMapper mapper)
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
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

        /// <summary>
        /// Gets a wishlist by name
        /// </summary>
        /// <param name="wishListName">Wishlist to search</param>
        /// <response code="200">Requested wishlist</response>
        /// <response code="404">Wishlist does not exist</response>
        /// <returns></returns>
        [HttpGet("{wishListName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWishListByName([FromRoute] string wishListName)
        {
            var result = await _wishListRepository.GetWishListByNameAsync(wishListName);

            return result.Match(wishList =>
            {
                var mapping = _mapper.Map<WishListResponse>(wishList);
                return Ok(mapping.ToSingleResponse());
            }, error => error.GetResultFromError());
        }

        /// <summary>
        /// Creates a wishlist
        /// </summary>
        /// <remarks>A user can only have a maximum of three wishlists at the same time</remarks>
        /// <param name="request"></param>
        /// <response code="201">Wishlist created successfully</response>
        /// <response code="400">Wishlist already exists or user has reached maximum number of wishlists</response>
        [HttpPost]
        public async Task<IActionResult> CreateWishList([FromBody] CreateWishListRequest request)
        {
            var userId = HttpContext.GetUserId();
            (string wishListName, string description) = request;

            var wishlist = new WishList { Name = wishListName, Description = description, UserId = userId };
            var result = await _wishListRepository.CreateWishListAsync(wishlist);

            return result.Match(
                () => CreatedAtAction(nameof(GetWishListByName), new {wishListName = wishlist.Name}, wishlist),
                error => error.GetResultFromError());
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

