using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [Authorize(Roles = RoleNames.User)]
    [ApiController]
    [Route("api/v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves user cart
        /// </summary>
        /// <remarks>User must be authenticated</remarks>
        /// <response code="200">Current user cart</response>
        /// <response code="401">Unauthenticated user</response>
        /// <response code="403">Unauthorized user</response>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart is null) return await CreateCart();

            var mapping = _mapper.Map<CartResponse>(cart);
            return Ok(mapping.ToSingleResponse());
        }

        /// <summary>
        /// Creates cart for the current user
        /// </summary>
        /// <response code="200">Cart created</response>
        /// <response code="400">Maximum number of carts reached</response>
        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = new Cart { UserId = userId };
            bool created = await _cartRepository.CreateCartForUserAsync(userId, cart);
            if(!created) return BadRequest(new {Error = "User reached the maximum number of carts."});

            var mapping = _mapper.Map<Cart, CartResponse>(cart);
            return CreatedAtAction(nameof(GetUserCart), new {cartId = mapping.CartId}, mapping.ToSingleResponse());
        }

        /// <summary>
        /// Adds book to cart
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Book added to cart</response>
        /// <response code="400">Invalid book or cart</response>
        [HttpPost("books")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddBookToCartRequest request)
        {
            var result = await _cartRepository.AddBookToCartAsync(
                request.CartId, request.BookId, request.Quantity
            );
            return result.Match(Ok, error => error.GetResultFromError());
        }

        /// <summary>
        /// Updates book quantity in cart
        /// </summary>
        /// <param name="bookId">Book to update</param>
        /// <param name="request"></param>
        /// <response code="200">Book quantity updated</response>
        /// <response code="400">Invalid book or cart</response>
        [HttpPut("books/{bookId:guid}")]
        public async Task<IActionResult> UpdateBookInCart(
            [FromRoute] Guid bookId, [FromBody] UpdateBookInCartRequest request)
        {
            bool updated = await _cartRepository.UpdateBookInCartAsync(
                request.CartId, bookId, request.Quantity
            );
            if (!updated) return BadRequest("Unable to update book in cart");

            return Ok();
        }

        /// <summary>
        /// Removes book from cart
        /// </summary>
        /// <param name="bookId">Book to remove</param>
        /// <param name="request"></param>
        /// <response code="200">Book removed</response>
        /// <response code="400">Invalid cart or book</response>
        [HttpDelete("books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromCart(
            [FromRoute] Guid bookId, [FromBody] RemoveBookFromCartRequest request)
        {
            var deleted = await _cartRepository.RemoveBookFromCartAsync(
                request.CartId, bookId
            );
            if(!deleted) return BadRequest("Unable to delete book from cart");

            return Ok();
        }
    }
}
