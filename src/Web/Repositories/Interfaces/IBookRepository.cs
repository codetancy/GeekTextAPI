using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync(string genre = null);
        Task<List<Book>> GetBooksByGenreAsync(string genreName);
        Task<Book> GetBookByIdAsync(int bookId);
    }
}
