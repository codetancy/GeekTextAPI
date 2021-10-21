using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerBookRepository : IBookRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerBookRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> BookExistsAsync(Guid bookId)
        {
            return await _dbContext.Books.AsNoTracking().AnyAsync(b => b.Id == bookId);
        }

        public async Task<bool> IsbnExistsAsync(string isbn)
        {
            return await _dbContext.Books.AsNoTracking().AnyAsync(b => b.Isbn == isbn);
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<List<Book>> GetBooksByGenreAsync(string genreName)
        {
            return await _dbContext.Books.Where(book => book.GenreName == genreName).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(Guid bookId)
        {
            return await _dbContext.Books.SingleOrDefaultAsync(book => book.Id == bookId);
        }

        public async Task<Book> GetBookByIsbnAsync(string bookIsbn)
        {
            /*
             * TODO: Mohamed - Implement getting a book by Isbn
             * Same implementation as GetBookByIdAsync, but in this case compare by Isbn
             */
            return await _dbContext.Books.SingleOrDefaultAsync(book => book.Isbn == bookIsbn);
        }

        public async Task<bool> CreateBookAsync(Book book)
        {
            /*
             * TODO: Mohamed - Implement adding a book
             * First, add the passed book with _dbContext.Books.AddAsync()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return true if any record was modified, else false
             */
            await _dbContext.Books.AddAsync(book);
            int changed = await _dbContext.SaveChangesAsync();
            return changed > 0;
        }

        public Task<bool> UpdateBookAsync(Book book) => throw new NotImplementedException();

        public async Task<bool> DeleteBookAsync(Guid bookId)
        {
            /*
             * TODO: Mohamed - Implement deleting a book
             * First, get the book by Id
             * Then, remove the author using _dbContext.Books.Remove()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly, return true if any record was modified, else false
             */
            var booktoDelete = await GetBookByIdAsync(bookId);

            if (booktoDelete == null)
                return false;

            _dbContext.Books.Remove(booktoDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
