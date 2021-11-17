using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Errors;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerCartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerCartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        private async Task<bool> UpdateCartPricesAsync(Guid cartId)
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

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Result> AddBookToCartAsync(Guid cartId, Guid bookId, int quantity = 1)
        {
            bool cartExists = await _dbContext.Carts.AsNoTracking().AnyAsync(c => c.CartId == cartId);
            if (!cartExists) return new Result(new CartDoesNotExist(cartId));

            bool bookExists = await _dbContext.Books.AsNoTracking().AnyAsync(b => b.Id == bookId);
            if (!bookExists) return new Result(new BookDoesNotExist(bookId));

            bool bookIsAlreadyInCart = await _dbContext.CartBooks.AsNoTracking()
                .AnyAsync(c => c.CartId == cartId && c.BookId == bookId);
            if (bookIsAlreadyInCart) return new Result(new BookAlreadyInCart(bookId, cartId));

            var cartBook = await _dbContext.CartBooks.AddAsync(new CartBook(cartId, bookId) {Quantity = quantity});
            await _dbContext.Entry(cartBook.Entity).Reference(cb => cb.Book).LoadAsync();
            bool saved = await UpdateCartPricesAsync(cartId);

            return saved
                ? new Result(null)
                : new Result(new UnableToAddBookToCart(cartId, bookId));
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
