using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        Task<List<WishList>> GetUserWishListsAsync(Guid userId);
        Task<WishList> GetWishListByNameAsync(string wishListName);
        Task<WishList> CreateWishListAsync(string wishListName, Guid userId);
        Task<bool> DeleteWishListAsync(string wishListName);
        Task<bool> AddBookToWishListAsync(string wishListName, Guid bookId);
        Task<bool> RemoveBookFromWishListAsync(string wishListName, Guid bookId);
    }
}
