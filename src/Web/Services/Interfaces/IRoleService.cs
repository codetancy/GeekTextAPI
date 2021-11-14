using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<Result> CreateRoleAsync(string roleName);
        Task<Result> DeleteRoleAsync(string roleName);
        Task<Result> AddUserToRole(string userName, string roleName);
        Task<Result> RemoveUserFromRole(string userName, string roleName);

    }
}
