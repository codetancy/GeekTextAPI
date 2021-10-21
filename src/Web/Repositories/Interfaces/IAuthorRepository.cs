using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<bool> AuthorExistsAsync(Guid authorId);
        Task<bool> AuthorsExistAsync(IEnumerable<Guid> authorsIds);
        Task<List<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(Guid authorId);
        Task<bool> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(Guid authorId);
    }
}
