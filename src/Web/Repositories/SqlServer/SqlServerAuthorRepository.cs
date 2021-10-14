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

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(Guid authorId)
        {
            return await _dbContext.Authors.SingleOrDefaultAsync(author => author.Id == authorId);
        }

        public Task<bool> CreateAuthorAsync(Author author)
        {
            /*
             * TODO: Logan - Implement adding an author
             * First, add the passed author with _dbContext.Authors.AddAsync()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return true if any record was modified, else false
             */
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAuthorAsync(Author author) => throw new NotImplementedException();

        public Task<bool> DeleteAuthorAsync(Guid authorId)
        {
            /*
             * TODO: Logan - Implement deleting an author
             * First, get the author by Id
             * Then, remove the author using _dbContext.Authors.Remove()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly, return true if any record was modified, else false
             */
            throw new NotImplementedException();
        }
    }
}
