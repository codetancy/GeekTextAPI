﻿using System.Collections.Generic;
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
                    Id = 1,
                    Isbn = "ABC-123",
                    Authors = new(),
                    Price = 1.2m,
                    Genre = new Genre(1, "Action"),
                    Synopsis = "First Book"
                    Copies = 5000,
                    Yearpublished = 2011,
                    Publisher = "FIU Publisher"
                },
                new Book
                {
                    Id = 2,
                    Isbn = "DEF-456",
                    Authors = new(),
                    Price = 4.2m,
                    Genre = new Genre(2, "Romance"),
                    Synopsis = "Second Book"
                    Copies = 8000,
                    Yearpublished = 2005,
                    Publisher = "Jackie Publishing"
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

        public async Task<List<Book>> GetBooksByGenreAsync(string genreName) {
            var books = _books.Where(b => b.Genre.Name == genreName).ToList();
            return await Task.FromResult(books);
        }
    }
}
