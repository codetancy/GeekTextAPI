using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        Task<bool> UserOwnsWishList(string wishListName, Guid userId);
        Task<Result<List<WishList>>> GetUserWishListsAsync(Guid userId);
        Task<Result<WishList>> GetWishListByNameAsync(string wishListName);
        Task<Result> CreateWishListAsync(WishList wishList);
        Task<Result> DeleteWishListAsync(string wishListName);
        Task<Result> AddBookToWishListAsync(string wishListName, Guid bookId);
        Task<Result> RemoveBookFromWishListAsync(string wishListName, Guid bookId, Guid cartId);
    }
}
