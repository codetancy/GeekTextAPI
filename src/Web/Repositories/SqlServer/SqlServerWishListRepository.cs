using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Constants;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerWishListRepository : IWishListRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerWishListRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> WishListExists(string wishListName)
        {
            var wishList = await _dbContext.WishLists.AsNoTracking().SingleOrDefaultAsync(w => w.Name == wishListName);
            return wishList is not null;
        }

        public async Task<bool> UserOwnsWishList(string wishListName, Guid userId)
        {
            var wishList = await _dbContext.WishLists.AsNoTracking().SingleOrDefaultAsync(w => w.Name == wishListName);
            if (wishList is null) return false;

            return wishList.UserId == userId;
        }

        public async Task<bool> UserExceedsWishListsLimit(Guid userId)
        {
            int count = await _dbContext.WishLists.AsNoTracking().Where(w => w.UserId == userId).CountAsync();
            return count >= WishListConstants.MaxWishListsPerUser;
        }

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await _dbContext
                .WishLists
                .Include(w => w.WishListBooks)
                .ThenInclude(wb => wb.Book)
                .Where(wishlist => wishlist.UserId == userId)
                .ToListAsync();
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            return await _dbContext.WishLists
                .Include(w => w.WishListBooks)
                .ThenInclude(wb => wb.Book)
                .SingleOrDefaultAsync(wishlist => wishlist.Name == wishListName);
        }

        public async Task<bool> CreateWishListAsync(WishList wishList)
        {
            int count = await _dbContext.WishLists.Where(w => w.UserId == wishList.UserId).CountAsync();
            if (count >= 3) return false;

            await _dbContext.WishLists.AddAsync(wishList);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0;
        }

        public async Task<bool> DeleteWishListAsync(string wishListName)
        {
            var wishlistToDelete = await _dbContext.WishLists.SingleOrDefaultAsync(w => w.Name == wishListName);
            _dbContext.WishLists.Remove(wishlistToDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> AddBookToWishListAsync(string wishListName, Guid bookId)
        {
            var wishList = await GetWishListByNameAsync(wishListName);
            if (wishList is null)
                return false;

            bool wishListContainsBook = wishList.WishListBooks.Any(wb => wb.BookId == bookId);
            if(!wishListContainsBook)
            {
                wishList.WishListBooks.Add(new WishListBook(wishListName, bookId));
            }

            _dbContext.WishLists.Update(wishList);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0;

        }

        public async Task<bool> RemoveBookFromWishListAsync(string wishListName, Guid bookId)
        {
            var wishList = await GetWishListByNameAsync(wishListName);
            if (wishList is null)
                return false;

            bool wishListContainsBook = wishList.WishListBooks.Any(wb => wb.BookId == bookId);
            if (wishListContainsBook)
            {
                var bookToRemove = wishList.WishListBooks.Single(wb => wb.BookId == bookId);
                wishList.WishListBooks.Remove(bookToRemove);
            }

            _dbContext.WishLists.Update(wishList);
            int changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }
    }
}
