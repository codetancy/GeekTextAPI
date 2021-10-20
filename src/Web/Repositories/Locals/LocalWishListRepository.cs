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
                    WishListBooks = new List<WishListBook>
                    {
                        new WishListBook
                        {
                            WishListName = "Fiction Books",
                            Book = new Book
                            {
                                Id = Guid.Parse("6c49814a-c3ce-4947-8fa4-993f37bc31d1"),
                                Title = "The Hunger Games"
                            },
                        },
                        new WishListBook
                        {
                            WishListName = "Fiction Books",
                            Book = new Book
                            {
                                Id = Guid.Parse("6c4fe33d-2f6c-4768-bace-32fe5127e0a4"),
                                Title = "Harry Potter: The Prisoner of Azkaban"
                            },
                        },
                    }
                }
            };
        }

        public Task<bool> UserOwnsWishList(string wishListName, Guid userId) => throw new NotImplementedException();

        public async Task<List<WishList>> GetUserWishListsAsync(Guid userId)
        {
            return await Task.FromResult(_wishList);
        }

        public async Task<WishList> GetWishListByNameAsync(string wishListName)
        {
            var wishList = _wishList.SingleOrDefault(w => w.Name == wishListName);
            return await Task.FromResult(wishList);
        }

        public Task<bool> CreateWishListAsync(WishList wishList) => throw new NotImplementedException();

        public Task<bool> DeleteWishListAsync(string wishListName) => throw new NotImplementedException();

        public Task<bool> AddBookToWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();

        public Task<bool> RemoveBookFromWishListAsync(string wishListName, Guid bookId) => throw new NotImplementedException();
    }
}
