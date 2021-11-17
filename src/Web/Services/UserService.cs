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
        private readonly IIdentityService _identityService;

        public UserService(UserManager<ApplicationUser> userManager, IIdentityService identityService)
        {
            _userManager = userManager;
            _identityService = identityService;
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

        private async Task<bool> UserNameIsTakenAsync(string userName)
            => await _userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);

        public async Task<Result<string>> UpdateUserAsync(string userName, ApplicationUser updatedUser)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null) return new Result<string>(new UserDoesNotExist(userName));

            if (user.UserName != updatedUser.UserName)
            {
                bool userNameIsTaken = await UserNameIsTakenAsync(updatedUser.UserName);
                if (userNameIsTaken) return new Result<string>(new UserNameAlreadyTaken(updatedUser.UserName));
                user.UserName = updatedUser.UserName;
            }

            if (user.PhoneNumber != updatedUser.PhoneNumber)
                user.PhoneNumber = updatedUser.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) new Result<string>(new UnableToUpdate("User"));

            string token = await _identityService.GenerateJwtToken(user);
            return new Result<string>(token);
        }
    }
}
