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

        private async Task<Cart> GetCartById(Guid cartId)
        {
            return await _dbContext.Carts
                .Include(c => c.CartBooks)
                .ThenInclude(cb => cb.Book)
                .SingleOrDefaultAsync(c => cartId == c.CartId);
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            return await _dbContext.Carts
            .Include(c => c.CartBooks)
            .ThenInclude(cb => cb.Book)
            .SingleOrDefaultAsync(c => c.UserId == userId);
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

        private async Task UpdateCartPricesAsync(Guid cartId)
        {
            var cart = _dbContext.Carts
                .Include(c => c.CartBooks)
                .ThenInclude(cb => cb.Book)
                .Single(c => c.CartId == cartId);

            foreach (var cartBook in cart.CartBooks)
            {
                cartBook.Price = cartBook.Quantity * cartBook.Book.UnitPrice;
            }

            cart.Subtotal = cart.CartBooks.Select(cb => cb.Price).Sum();
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AddBookToCartAsync(Guid cartId, Guid bookId, int quantity = 1)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var cart = await _dbContext.Carts
                    .Include(c => c.CartBooks)
                    .SingleAsync(c => c.CartId == cartId);

                if (cart.CartBooks.Any(cb => cb.BookId == bookId))
                    throw new Exception();

                cart.CartBooks.Add(new CartBook(cartId, bookId){Quantity = quantity});

                await _dbContext.SaveChangesAsync();

                await UpdateCartPricesAsync(cartId);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateBookInCartAsync(Guid cartId, Guid bookId, int quantity)
        {
            if (quantity == 0)
                return await RemoveBookFromCartAsync(cartId, bookId);

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var cartBook = await _dbContext.CartBooks
                    .Where(cb => cb.CartId == cartId && cb.BookId == bookId)
                    .SingleOrDefaultAsync();
                
                cartBook.Quantity = quantity;

                await _dbContext.SaveChangesAsync();

                await UpdateCartPricesAsync(cartId);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveBookFromCartAsync(Guid cartId, Guid bookId)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var cartBook = await _dbContext.CartBooks
                    .Where(cb => cb.CartId == cartId && cb.BookId == bookId)
                    .SingleOrDefaultAsync();
                
                _dbContext.CartBooks.Remove(cartBook);

                await _dbContext.SaveChangesAsync();

                await UpdateCartPricesAsync(cartId);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

            return true;
        }
    }
}
