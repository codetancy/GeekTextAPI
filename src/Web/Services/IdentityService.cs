using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Web.Data.Identities;
using Web.Models;
using Web.Options;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(UserManager<ApplicationUser> userManager, JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return new AuthenticationResult { Succeed = false, Errors = new[] {$"No user with email {email}"} };

            bool valid = await _userManager.CheckPasswordAsync(user, password);

            return valid
                ? new AuthenticationResult { Succeed = true, Token = GenerateJwtToken(user) }
                : new AuthenticationResult { Succeed = false, Errors = new[] {"Incorrect credentials"} };
        }

        public async Task<AuthenticationResult> SignupAsync(string email, string userName, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser is not null)
                return new AuthenticationResult { Succeed = false, Errors = new[] {"Email already in use"} };

            var user = new ApplicationUser { Email = email, UserName = userName };
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded
                ? new AuthenticationResult { Succeed = true, Token = GenerateJwtToken(user) }
                : new AuthenticationResult { Succeed = false, Errors = result.Errors.Select(err => err.Description) };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()), new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public async Task<bool> UsernameExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user is not null;
        }

        public async Task<AuthenticationResult> UsernameBelongsToCurrentUser(string username, Guid userId)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return new AuthenticationResult {Succeed = false, Errors = new[] {$"No user with username {username}"}};

            return user.Id == userId
                ? new AuthenticationResult {Succeed = true}
                : new AuthenticationResult {Succeed = false, Errors = new[] {$"You are not {username}"}};
        }
    }
}
