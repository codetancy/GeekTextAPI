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

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await _dbContext.WishLists.Where(wishlist => wishlist.UserId == userId).ToListAsync();
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            return await _dbContext.WishLists.SingleOrDefaultAsync(wishlist => wishlist.Name == wishListName);
        }

        public Task<WishList> CreateWishListAsync(string wishListName, Guid userId)
        {
            /*
             * TODO: Ricardo - Implement adding an wishlist for a user
             * First, get the number of wishlists the user has with _dbContext.WishLists.Where().Count()
             * Then, verify the count does not exceed 3, if the count exceeds 3 return null
             * Else, add the wishlist with _dbContext.Wishlists.AddAsync()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return the created wishlist
             */
            await _dbContext.WishLists.Where(userId).Count();

            await _dbContext.WishLists.AddAsync();
            int changed = await _dbContext.SaveChangesAsync();
            return changed > 0; 

        }

        public Task<bool> DeleteWishListAsync(string wishListName)
        {
            /*
             * TODO: Ricardo - Implement deleting a wishlist
             * First, get the wishlist from its name
             * Then, remove the wishList using _dbContext.WishLists.Remove()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly, return true if any record was modified, else false
             */
            var wishListToDelete = await GetWishListByNameAsync(wishListName);
            if (wishListToDelete is null)
                return false;

            _dbContext.WishLists.Remove(WishListToDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }

        public Task<bool> AddBookToWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();

        public Task<bool> RemoveBookFromWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();
    }
}
