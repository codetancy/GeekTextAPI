using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Locals
{
    public class LocalAuthorRepository
    {
        private readonly List<Author> _author;

        public LocalAuthorRepository()
        {
            _author = new List<Author>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Forename = "Joanne",
                    Surname = "Rowling",
                    PenName = "J.K. Rowling"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Forename = "Gabriel",
                    Surname = "Garcia Marquez",
                    PenName = "Gabriel Garcia Marquez"
                }
            };
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await Task.FromResult(_author);
        }

        public async Task<Author> GetAuthorByIdAsync(Guid authorId)
        {
            var author = _author.SingleOrDefault(a => a.Id == authorId);
            return await Task.FromResult(author);
        }

        public Task<bool> CreateAuthorAsync(Author author) => throw new NotImplementedException();

        public Task<bool> UpdateAuthorAsync(Author author) => throw new NotImplementedException();

        public Task<bool> DeleteAuthorAsync(Guid authorId) => throw new NotImplementedException();
    }
}
