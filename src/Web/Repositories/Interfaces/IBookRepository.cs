using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBooksAsync();
        Task<List<Book>> GetBooksByGenreAsync(string genreName);
        Task<Book> GetBookByIdAsync(Guid bookId);
        Task<Book> GetBookByIsbnAsync(string bookIsbn);
        Task<bool> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(Guid bookId);
    }
}
