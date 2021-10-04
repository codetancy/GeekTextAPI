using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalAuthorRepository : IAuthorRepository
    {
        private readonly List<Author> _author;

        public LocalAuthorRepository()
        {
            _author = new List<Author>
            {
                new()
                {
                    Id = 1,
                },
                new()
                {
                    Id = 2,
                }
            };
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await Task.FromResult(_author);
        }

        public async Task<Author> GetAuthorsByIdAsync(int authorId)
        {
            var author = _author.SingleOrDefault(author => author.Id == authorId);
            return await Task.FromResult(author);
        }
    }
}
