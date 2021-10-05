using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalCartRepository : ICartRepository
    {
        private readonly List<Cart> _cart;
        private readonly IBookRepository _bookRepository;
        public LocalCartRepository(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _cart = new List<Cart> 
            {
                new()
                {
                    UserId = Guid.Parse("3304f724-dbfd-45f3-9737-ccd0b28929d6"),
                    CartId = Guid.Parse("3304f724-dbfd-45f3-9737-ccd0b28929d6"),
                    Subtotal = 99.95,
                    Books = new List<Book>
                    {
                        new Book { Id = 1},
                        new Book { Id = 2}
                    }
                }
            };
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid cartUserID)
        {
            var cart = _cart.SingleOrDefault(cart => cart.UserId == cartUserID);
            return await Task.FromResult(cart);
        }
        
        public async Task<List<Book>> AddBookToCart(Guid cartId, int bookId)
        {   
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if(book == null)
            {
                return null;
            }
            var cart = _cart.SingleOrDefault(cart => cartId == cart.CartId);
            cart.Books.Add(new Book { Id = bookId });
            return await Task.FromResult(cart.Books);
        }
    }
}
