using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
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
            var userId = Guid.Parse("3304f724-dbfd-45f3-9737-ccd0b28929d6");
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        // POST api/v1/cart/books/{bookId}
        [HttpPost("books/{bookId:int}")]
        public async Task<IActionResult> AddBookToCart([FromRoute] int bookId)
        {
            var cartId = Guid.Parse("3304f724-dbfd-45f3-9737-ccd0b28929d6");
            var books = await _cartRepository.AddBookToCart(cartId, bookId);

            if(books == null){
                return BadRequest($"There's not a book with ID {bookId}");
            }
            return Ok(books);
        }
    }
}
