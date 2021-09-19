using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalBookRepository : IBookRepository
    {
        private readonly List<Book> _books;

        public LocalBookRepository()
        {
            _books = new List<Book>
            {
                new()
                {
                    Id = 1,
                    Isbn = "ABC-123",
                    Authors = new(),
                    Price = 1.2m,
                    Synopsis = "First Book"
                },
                new()
                {
                    Id = 2,
                    Isbn = "DEF-456",
                    Authors = new(),
                    Price = 4.2m,
                    Synopsis = "Second Book"
                }
            };
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await Task.FromResult(_books);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            var book = _books.SingleOrDefault(book => book.Id == bookId);
            return await Task.FromResult(book);
        }


    }
}
