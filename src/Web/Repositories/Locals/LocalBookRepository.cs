using System;
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
                new Book
                {
                    Id = Guid.Parse("6c49814a-c3ce-4947-8fa4-993f37bc31d1"),
                    Title = "The Hunger Games",
                    Isbn = "ABC-123",
                    UnitPrice = 1.2m,
                    Genre = new Genre { Name = "Dystopic" },
                    Synopsis = "First Book",
                    CopiesSold = 5000,
                    YearPublished = 2011,
                },
                new Book
                {
                    Id = Guid.Parse("6c4fe33d-2f6c-4768-bace-32fe5127e0a4"),
                    Title = "Harry Potter: The Prisoner of Azkaban",
                    Isbn = "DEF-456",
                    UnitPrice = 4.2m,
                    Genre = new Genre { Name = "Magic" },
                    Synopsis = "Second Book",
                    CopiesSold = 8000,
                    YearPublished = 2005,
                }
            };
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await Task.FromResult(_books);
        }

        public async Task<Book> GetBookByIdAsync(Guid bookId)
        {
            var book = _books.SingleOrDefault(b => b.Id == bookId);
            return await Task.FromResult(book);
        }

        public async Task<List<Book>> GetBooksByGenreAsync(string genreName) {
            var books = _books.Where(b => b.Genre.Name == genreName).ToList();
            return await Task.FromResult(books);
        }
    }
}
