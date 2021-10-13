using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalWishListRepository : IWishListRepository
    {
        private readonly List<WishList> _wishList;

        public LocalWishListRepository()
        {
            _wishList = new List<WishList>
            {
                new()
                {
                    UserId = Guid.NewGuid(),
                    Name = "Fiction Books",
                    Description = "Fiction Books",
                    Books = new List<Book>
                    {
                        new Book { Id = Guid.NewGuid() },
                        new Book { Id = Guid.NewGuid() }
                    }
                }
            };
        }

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await Task.FromResult(_wishList);
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            var wishList = _wishList.SingleOrDefault(w => w.Name == wishListName);
            return await Task.FromResult(wishList);
        }
    }
}
