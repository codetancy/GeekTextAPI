using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerCartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;

        public SqlServerCartRepository(AppDbContext dbContext, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            return await _dbContext.Carts.Include(c => c.CartBooks).SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<bool> CreateCartForUserAsync(Guid userId, Cart cart)
        {
            int userCarts = await _dbContext.Carts.Where(c => c.UserId == userId).CountAsync();
            if (userCarts >= 1)
                return false;

            await _dbContext.Carts.AddAsync(cart);
            int added = await _dbContext.SaveChangesAsync();
            return added > 0;
        }

        public async Task<List<CartBook>> AddBookToCart(Guid cartId, Guid bookId, int quantity = 1)
        {
            var cart = _dbContext.Carts.Include(c => c.CartBooks).SingleOrDefault(c => cartId == c.CartId);
            if (cart == null) return null;

            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null) return null;

            bool cartContainsBook = cart.CartBooks.Any(b => b.Book.Id == bookId);
            if (cartContainsBook) {
                var cartBook = cart.CartBooks.Single(cb => cb.Book.Id == bookId);
                cartBook.Quantity += quantity;
            }
            else {
                cart.CartBooks.Add(new CartBook { CartId = cartId, BookId = bookId });
            }

            _dbContext.Carts.Update(cart);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0 ? cart.CartBooks.ToList() : null;
        }

        public async Task<List<CartBook>> RemoveBookFromCart(Guid cartId, Guid bookId)
        {
            var cart = _dbContext.Carts.Include(c => c.CartBooks).SingleOrDefault(c => cartId == c.CartId);
            if (cart == null) return null;

            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null) return null;

            bool cartContainsBook = cart.CartBooks.Any(b => b.Book.Id == bookId);
            if (!cartContainsBook) return null;

            var cartBook = cart.CartBooks.Single(cb => cb.Book.Id == bookId);
            cart.CartBooks.Remove(cartBook);

            _dbContext.Carts.Update(cart);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0 ? cart.CartBooks.ToList() : null;
        }
    }
}
