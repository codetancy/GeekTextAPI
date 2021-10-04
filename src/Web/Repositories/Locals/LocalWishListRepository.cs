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
                    Name = "Fiction Books",
                    Description = "Fiction Books"
                }
            };
        }

        public async Task<List<WishList>> GetWishListsAsync()
        {
            return await Task.FromResult(_wishList);
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            var wishList = _wishList.SingleOrDefault(wishList => wishList.Name == wishListName);
            return await Task.FromResult(wishList);
        }

        Task<List<WishList>> IWishListRepository.GetWishListAsync() => throw new NotImplementedException();
        Task<WishList> IWishListRepository.GetWishListByNameAsync(string wishListName) => throw new NotImplementedException();
    }
}
