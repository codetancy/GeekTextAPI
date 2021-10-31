using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerGenreRepository : IGenreRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerGenreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GenreExistsAsync(string genreName)
        {
            return await _dbContext.Genres.AsNoTracking().AnyAsync(g => g.Name == genreName);
        }

        public async Task<List<Genre>> GetAllGenresAsync(PaginationFilter filter = null)
        {
            if (filter is null)
                return await _dbContext.Genres.ToListAsync();

            int skipNumber = (filter.PageNumber - 1) * filter.PageSize;
            return await _dbContext.Genres
                .OrderBy(g => g.Name)
                .Skip(skipNumber)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<bool> CreateGenreAsync(Genre genre)
        {
            await _dbContext.Genres.AddAsync(genre);
            int added = await _dbContext.SaveChangesAsync();
            return added > 0;
        }
    }
}
