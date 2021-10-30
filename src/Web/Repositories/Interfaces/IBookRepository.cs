using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<bool> BookExistsAsync(Guid bookId);
        Task<bool> IsbnExistsAsync(string isbn);
        Task<List<Book>> GetBooksAsync(BookSearchFilter filter = null);
        Task<List<Book>> GetTopSellersAsync();
        Task<List<Book>> GetBooksByGenreAsync(string genreName);
        Task<Book> GetBookByIdAsync(Guid bookId);
        Task<Book> GetBookByIsbnAsync(string bookIsbn);
        Task<bool> CreateBookAsync(Book book, List<Guid> authorsIds = null);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(Guid bookId);
    }
}
