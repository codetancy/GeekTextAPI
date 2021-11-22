using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        Task<bool> WishListExists(string wishListName);
        Task<bool> UserOwnsWishList(string wishListName, Guid userId);
        Task<bool> UserExceedsWishListsLimit(Guid userId);
        Task<List<WishList>> GetUserWishListsAsync(Guid userId);
        Task<WishList> GetWishListByNameAsync(string wishListName);
        Task<bool> CreateWishListAsync(WishList wishList);
        Task<Result> DeleteWishListAsync(string wishListName);
        Task<Result> AddBookToWishListAsync(string wishListName, Guid bookId);
        Task<Result> RemoveBookFromWishListAsync(string wishListName, Guid bookId, Guid cartId);
    }
}
