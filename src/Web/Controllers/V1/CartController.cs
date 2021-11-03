using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
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
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        // GET api/v1/cart
        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if(cart is null) return NotFound("There is no cart for the current user");

            var mapping = _mapper.Map<Cart, CartResponse>(cart);
            return Ok(mapping.ToResponse());
        }

        // POST api/v1/cart
        [HttpPost]
        public async Task<IActionResult> CreateCart()
        {
            var userId = HttpContext.GetUserId();
            var cart = new Cart { UserId = userId };
            bool created = await _cartRepository.CreateCartForUserAsync(userId, cart);
            if(!created) return BadRequest(new {Error = "User already reached maximum number of carts."});

            var mapping = _mapper.Map<Cart, CartResponse>(cart);
            return CreatedAtAction(nameof(GetUserCart), new {cartId = mapping.CartId}, mapping.ToResponse());
        }

        // POST api/v1/cart/books
        [HttpPost("books")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddBookToCartRequest request)
        {
            (Guid bookId, Guid cartId, int quantity) = request;
            var cart = await _cartRepository.AddBookToCart(cartId, bookId, quantity);

            if (cart is null) return NotFound($"Book with ID {bookId} does not exist.");

            var mapping = _mapper.Map<Cart, CartResponse>(cart);
            return Ok(mapping.ToResponse());
        }

        [HttpDelete("books/{bookId:guid}")]
        public async Task<IActionResult> RemoveBookFromCart(
            [FromRoute] Guid bookId, [FromBody] RemoveBookFromCartRequest request)
        {
            var cart = await _cartRepository.RemoveBookFromCart(request.CartId, bookId);

            if(cart is null) return BadRequest(new {Error = "Unable to delete book from cart"});

            var mapping = _mapper.Map<Cart, CartResponse>(cart);
            return Ok(mapping.ToResponse());
        }
    }
}
