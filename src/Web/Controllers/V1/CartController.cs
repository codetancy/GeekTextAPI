using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // GET api/v1/cart
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return cart is null ? NotFound("There is no cart for the current user") : Ok(cart);
        }

        // POST api/v1/cart
        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = new Cart { UserId = userId };
            bool created = await _cartRepository.CreateCartForUserAsync(userId, cart);
            return created
                ? CreatedAtAction(nameof(GetUserCart), new {cartId = cart.CartId}, cart)
                : BadRequest();
        }

        // POST api/v1/cart/books
        [HttpPost("books")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddBookToCartRequest request)
        {
            (Guid bookId, Guid cartId, int quantity) = request;
            var books = await _cartRepository.AddBookToCart(cartId, bookId, quantity);

            if (books is null) return NotFound($"Book with ID {bookId} does not exist.");

            
        }

        [HttpDelete("books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromCart(
            [FromRoute] Guid bookId, [FromBody] RemoveBookFromCartRequest request)
        {
            var books = await _cartRepository.RemoveBookFromCart(request.CartId, bookId);

            return books == null
                ? BadRequest()
                : Ok(books);
        }
    }
}
