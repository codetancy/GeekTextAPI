using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalCartRepository : ICartRepository
    {
        private readonly List<Cart> _cart;
        public LocalCartRepository()
        {
            _cart = new List<Cart> 
            {
                new()
                {
                    UserId = Guid.NewGuid(),
                    CartId = Guid.NewGuid(),
                    Subtotal = 99.95,
                    Books = new List<Book>
                    {
                        new Book { Id = 1},
                        new Book { Id = 2}
                    }
                }
            };
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid cartUserID)
        {
            var cart = _cart.SingleOrDefault(cart => cart.UserId == cartUserID);
            return await Task.FromResult(cart);
        }
        
        public async 
    }
}
