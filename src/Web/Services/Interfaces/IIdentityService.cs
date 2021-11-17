using System;
using System.Threading.Tasks;
using Web.Data.Identities;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string input, string password);
        Task<AuthenticationResult> SignupAsync(string email, string userName, string password);
        Task<string> GenerateJwtToken(ApplicationUser user);
        Task<AuthenticationResult> UserNameBelongsToUserAsync(string username, Guid userId);
        Task<ApplicationUser> GetUserByNameAsync(string username);
    }
}
