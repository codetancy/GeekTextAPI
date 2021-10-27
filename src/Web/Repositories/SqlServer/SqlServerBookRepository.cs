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
            return await _dbContext.Books.Include(book => book.Authors)
                .SingleOrDefaultAsync(book => book.Id == bookId);
        }

        public async Task<Book> GetBookByIsbnAsync(string bookIsbn)
        {
            return await _dbContext.Books.SingleOrDefaultAsync(book => book.Isbn == bookIsbn);
        }

        public async Task<bool> CreateBookAsync(Book book, List<Guid> authorsIds = null)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await _dbContext.Books.AddAsync(book);
                await _dbContext.SaveChangesAsync();

                if (authorsIds?.Any() ?? false)
                {
                    book.BookAuthors =
                        authorsIds.Select(authorId => new BookAuthor {BookId = book.Id, AuthorId = authorId}).ToList();
                    await _dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

            return true;
        }

        public Task<bool> UpdateBookAsync(Book book) => throw new NotImplementedException();

        public async Task<bool> DeleteBookAsync(Guid bookId)
        {
            var bookToDelete = await GetBookByIdAsync(bookId);
            if (bookToDelete == null) return false;

            _dbContext.Books.Remove(bookToDelete);
            int deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
