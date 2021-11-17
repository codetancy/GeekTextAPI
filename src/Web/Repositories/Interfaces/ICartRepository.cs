using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(Guid userId);
        Task<bool> CreateCartForUserAsync(Guid userId, Cart cart);
        Task<Result> AddBookToCartAsync(Guid cartId, Guid bookId, int quantity = 1);
        Task<bool> UpdateBookInCartAsync(Guid cartId, Guid bookId, int quantity);
        Task<bool> RemoveBookFromCartAsync(Guid cartId, Guid bookId);
    }
}
