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
    }
}
