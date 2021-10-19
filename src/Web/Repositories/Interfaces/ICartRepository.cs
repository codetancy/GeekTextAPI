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
        Task<List<CartBook>> AddBookToCart(Guid cartId, Guid bookId, int quantity = 1);
        Task<List<CartBook>> RemoveBookFromCart(Guid cartId, Guid bookId);
    }
}
