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
    public class SqlServerWishListRepository : IWishListRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerWishListRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> UserOwnsWishList(string wishListName, Guid userId)
        {
            var wishList = await _dbContext.WishLists.AsNoTracking().SingleOrDefaultAsync(w => w.Name == wishListName);
            if (wishList is null) return false;

            return wishList.UserId == userId;
        }

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await _dbContext.WishLists.Where(wishlist => wishlist.UserId == userId).ToListAsync();
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            return await _dbContext.WishLists.SingleOrDefaultAsync(wishlist => wishlist.Name == wishListName);
        }

        public async Task<bool> CreateWishListAsync(WishList wishList)
        {
            /*
             * TODO: Ricardo - Implement adding an wishlist for a user
             * First, get the number of wishlists the user has with _dbContext.WishLists.Where().Count()
             * Then, verify the count does not exceed 3, if the count exceeds 3 return null
             * Else, add the wishlist with _dbContext.Wishlists.AddAsync()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return the created wishlist
             */
            int count = await _dbContext.WishLists.Where(w => w.UserId == wishList.UserId).CountAsync();
            if (count >= 3) return false;

            await _dbContext.WishLists.AddAsync(wishList);
            int changed = await _dbContext.SaveChangesAsync();

            return changed > 0;
        }

        public async Task<bool> DeleteWishListAsync(string wishListName)
        {
            /*
             * TODO: Ricardo - Implement deleting a wishlist
             * First, get the wishlist from its name
             * Then, remove the wishList using _dbContext.WishLists.Remove()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly, return true if any record was modified, else false
             */
            var wishlistToDelete = await _dbContext.WishLists.SingleOrDefaultAsync(w => w.Name == wishListName);
            _dbContext.WishLists.Remove(wishlistToDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public Task<bool> AddBookToWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();

        public Task<bool> RemoveBookFromWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();
    }
}
