using System;
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
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<ApplicationUser>> GetUserAsync(Guid userId)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user is null)
                return new Result<ApplicationUser>(new UserDoesNotExist(userId));

            return new Result<ApplicationUser>(user);
        }

        public async Task<Result<ApplicationUser>> GetUserAsync(string userName)
        {
            var user = await _userManager.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.UserName == userName);

            if (user is null)
                return new Result<ApplicationUser>(new UserDoesNotExist(userName));

            return new Result<ApplicationUser>(user);
        }
    }
}
