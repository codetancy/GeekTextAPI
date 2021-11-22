using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Constants;
using Web.Data;
using Web.Errors;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerWishListRepository : IWishListRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly ICartRepository _cartRepository;

        public SqlServerWishListRepository(AppDbContext dbContext, ICartRepository cartRepository)
        {
            _dbContext = dbContext;
            _cartRepository = cartRepository;
        }

        public async Task<bool> UserOwnsWishList(string wishListName, Guid userId)
        {
            var wishList = await _dbContext.WishLists.AsNoTracking().SingleOrDefaultAsync(w => w.Name == wishListName);
            if (wishList is null) return false;

            return wishList.UserId == userId;
        }

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await _dbContext.WishLists
                .Include(w => w.WishListBooks)
                .ThenInclude(wb => wb.Book)
                .Where(wishlist => wishlist.UserId == userId)
                .ToListAsync();
        }

        public async Task<Result<WishList>> GetWishListByNameAsync(string wishListName)
        {
            var wishlist =  await _dbContext.WishLists
                .Include(w => w.WishListBooks)
                .ThenInclude(wb => wb.Book)
                .SingleOrDefaultAsync(wishlist => wishlist.Name == wishListName);

            return wishlist is null
                ? new Result<WishList>(new WishListDoesNotExist(wishListName))
                : new Result<WishList>(wishlist);
        }

        public async Task<Result> CreateWishListAsync(WishList wishList)
        {
            bool wishListExists = await _dbContext.WishLists.AsNoTracking().AnyAsync();
            if (wishListExists) return new Result(new WishListAlreadyExists(wishList.Name));

            int count = await _dbContext.WishLists.Where(w => w.UserId == wishList.UserId).CountAsync();
            if (count >= 3) return new Result(new UserReachedMaxNumOfWishLists());

            await _dbContext.WishLists.AddAsync(wishList);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0
                ? new Result(null)
                : new Result(new UnableToCreate(nameof(WishList)));
        }

        public async Task<Result> DeleteWishListAsync(string wishListName)
        {
            bool wishListExists = await _dbContext.WishLists.AsNoTracking().AnyAsync(w => w.Name == wishListName);
            if (!wishListExists) return new Result(new WishListDoesNotExist(wishListName));

            var wishlistToDelete = await _dbContext.WishLists.SingleAsync(w => w.Name == wishListName);
            _dbContext.WishLists.Remove(wishlistToDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0
                ? new Result(null)
                : new Result(new UnableToDelete(nameof(WishList)));
        }

        public async Task<Result> AddBookToWishListAsync(string wishListName, Guid bookId)
        {
            bool wishListExists = await _dbContext.WishLists.AsNoTracking().AnyAsync(w => w.Name == wishListName);
            if (!wishListExists) return new Result(new WishListDoesNotExist(wishListName));

            bool bookExists = await _dbContext.Books.AsNoTracking().AnyAsync(b => b.Id == bookId);
            if (!bookExists) return new Result(new BookDoesNotExist(bookId));

            bool wishListContainsBook = await _dbContext.WishListBooks
                .AnyAsync(wb => wb.WishListName == wishListName && wb.BookId == bookId);
            if (wishListContainsBook) return new Result(new WishListAlreadyContainsBook(wishListName, bookId));

            await _dbContext.WishListBooks.AddAsync(new WishListBook(wishListName, bookId));
            int changes = await _dbContext.SaveChangesAsync();

            return changes > 0
                ? new Result(null)
                : new Result(new UnableToAddBookToWishList(wishListName, bookId));

        }

        public async Task<Result> RemoveBookFromWishListAsync(string wishListName, Guid bookId, Guid cartId)
        {
            bool wishListExists = await _dbContext.WishLists.AsNoTracking().AnyAsync(w => w.Name == wishListName);
            if (!wishListExists) return new Result(new WishListDoesNotExist(wishListName));

            bool bookExists = await _dbContext.Books.AsNoTracking().AnyAsync(b => b.Id == bookId);
            if (!bookExists) return new Result(new BookDoesNotExist(bookId));

            var wishListBook = await _dbContext.WishListBooks.SingleOrDefaultAsync(
                wb => wb.WishListName == wishListName && wb.BookId == bookId);
            if (wishListBook is null) return new Result(new WishListDoesNotContainBook(wishListName, bookId));

            if (cartId != Guid.Empty)
            {
                var result = await _cartRepository.AddBookToCartAsync(cartId, bookId);
                var error = result.Match(() => null, error => error);
                if (error is not null) return new Result(error);
            }

            _dbContext.WishListBooks.Remove(wishListBook);
            int changes = await _dbContext.SaveChangesAsync();
            return changes > 0
                ? new Result(null)
                : new Result(new UnableToRemoveBookFromWishList(wishListName, bookId));
        }
    }
}
