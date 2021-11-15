using System;
using System.Threading.Tasks;
using Web.Data.Identities;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<ApplicationUser>> GetUserAsync(Guid userId);
        Task<Result<ApplicationUser>> GetUserAsync(string userName);
    }
}
