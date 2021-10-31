using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<bool> GenreExistsAsync(string genreName);
        Task<List<Genre>> GetAllGenresAsync(PaginationFilter filter = null);
        Task<bool> CreateGenreAsync(Genre genre);
    }
}
