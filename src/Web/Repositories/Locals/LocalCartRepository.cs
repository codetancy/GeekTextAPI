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
                    CartBooks = new List<CartBook>
                    {
                        new CartBook()
                        {
                            Book = new Book()
                            {
                                Id = Guid.Parse("6c49814a-c3ce-4947-8fa4-993f37bc31d1"),
                                Title = "The Hunger Games"
                            },
                            Quantity = 3
                        },
                        new CartBook()
                        {
                            Book = new Book()
                            {
                                Id = Guid.Parse("6c4fe33d-2f6c-4768-bace-32fe5127e0a4"),
                                Title = "Harry Potter: The Prisoner of Azkaban"
                            },
                            Quantity = 1
                        },
                    }
                }
            };
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid cartUserId)
        {
            var cart = _cart.SingleOrDefault(c => c.UserId == cartUserId);
            return await Task.FromResult(cart);
        }

        public async Task<List<CartBook>> AddBookToCart(Guid cartId, Guid bookId)
        {
            var cart = _cart.SingleOrDefault(c => cartId == c.CartId);
            if (cart == null)
            {
                return null;
            }

            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                return null;
            }

            bool cartContainsBook = cart.CartBooks.Any(b => b.Book.Id == bookId);
            if (cartContainsBook)
            {
                var cartBook = cart.CartBooks.Single(cb => cb.Book.Id == bookId);
                cartBook.Quantity += 1;
            }
            else
            {
                cart.CartBooks.Add(new CartBook { Book = new Book { Id = bookId } });
            }

            return await Task.FromResult(cart.CartBooks.ToList());
        }
    }
}
