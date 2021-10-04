using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IWishListRepository
    {

        Task<List<WishList>> GetWishListAsync();

        Task<WishList> GetWishListByNameAsync(string wishListName);
    }
}
