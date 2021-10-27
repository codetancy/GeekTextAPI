using System;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string input, string password);
        Task<AuthenticationResult> SignupAsync(string email, string userName, string password);
        Task<bool> UsernameExists(string username);
        Task<AuthenticationResult> UsernameBelongsToCurrentUser(string username, Guid userId);
    }
}
