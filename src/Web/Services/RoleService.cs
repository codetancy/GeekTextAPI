using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data.Identities;
using Web.Errors;
using Web.Models;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IEnumerable<string>> GetAllRolesAsync()
            => await _roleManager.Roles.Select(r => r.Name).ToListAsync();

        public async Task<Result> CreateRoleAsync(string roleName)
        {
            bool exists = await _roleManager.RoleExistsAsync(roleName);
            if (exists) return new Result(new RoleAlreadyExists(roleName));

            var result = await _roleManager.CreateAsync(new ApplicationRole {Name = roleName});

            return result.Succeeded
                ? new Result()
                : new Result(new UnableToCreate("Role"));
        }

        public async Task<Result> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null) return new Result(new RoleDoesNotExist(roleName));

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded
                ? new Result()
                : new Result(new UnableToDelete("Role"));
        }

        public async Task<Result> AddUserToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return new Result(new UserDoesNotExist(userName));

            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) return new Result(new RoleDoesNotExist(roleName));

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result.Succeeded
                ? new Result()
                : new Result(new UnableToCreate("User-role"));
        }

        public async Task<Result> RemoveUserFromRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return new Result(new UserDoesNotExist(userName));

            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) return new Result(new RoleDoesNotExist(roleName));

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            return result.Succeeded
                ? new Result()
                : new Result(new UnableToDelete("User-role"));
        }
    }
}
