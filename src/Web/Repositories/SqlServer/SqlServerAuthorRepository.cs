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
    public class SqlServerAuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerAuthorRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AuthorExistsAsync(Guid authorId)
        {
            return await _dbContext.Authors.AsNoTracking().AnyAsync(a => a.Id == authorId);
        }

        public async Task<bool> AuthorsExistAsync(IEnumerable<Guid> authorsIds)
        {
            var matches = await _dbContext.Authors.AsNoTracking()
                .Where(author => authorsIds.Contains(author.Id))
                .Select(author => author.Id)
                .ToListAsync();

            int invalidCount = matches.Except(authorsIds).Count();
            return invalidCount == 0;
        }

        public async Task<List<Author>> GetAllAuthorsAsync(PaginationFilter filter = null)
        {
            if (filter is null)
                return await _dbContext.Authors.Include(author => author.Books).ToListAsync();

            int skipSize = (filter.PageNumber - 1) * filter.PageSize;
            return await _dbContext.Authors
                .Include(author => author.Books)
                .OrderBy(author => author.Surname)
                .ThenBy(author => author.Forename)
                .Skip(skipSize).Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(Guid authorId)
        {
            return await _dbContext.Authors.Include(author => author.Books)
                .SingleOrDefaultAsync(author => author.Id == authorId);
        }

        public async Task<bool> CreateAuthorAsync(Author author)
        {
            await _dbContext.Authors.AddAsync(author);
            int changed = await _dbContext.SaveChangesAsync();
            return changed > 0;
        }

        public Task<bool> UpdateAuthorAsync(Author author) => throw new NotImplementedException();

        public async Task<bool> DeleteAuthorAsync(Guid authorId)
        {
            var authorToDelete = await _dbContext.Authors.SingleOrDefaultAsync(author => author.Id == authorId);
            if (authorToDelete is null) return false;

            _dbContext.Authors.Remove(authorToDelete);
            int deleted = await _dbContext.SaveChangesAsync();

            return deleted > 0;
        }
    }
}
