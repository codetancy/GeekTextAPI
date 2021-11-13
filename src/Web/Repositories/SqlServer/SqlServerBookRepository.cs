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

        public async Task<List<Book>> GetBooksAsync(BookSearchFilter filter = null)
        {
            if (filter is null)
                return await _dbContext.Books.ToListAsync();

            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.GenreName))
                query = query.Where(book => book.GenreName == filter.GenreName);

            if (filter.RatingGtEq is not null)
                query = query.Where(book => book.Rating >= filter.RatingGtEq);

            int skipSize = (filter.PageNumber - 1) * filter.PageSize;
            return await query
                .OrderBy(book => book.Title)
                .Skip(skipSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<List<Book>> GetTopSellersAsync()
        {
            return await _dbContext.Books.OrderByDescending(book => book.CopiesSold).Take(10).ToListAsync();
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
