using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Web.Constants;
using Web.Data.Identities;
using Web.Models;
using Web.Options;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            JwtOptions jwtOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return new AuthenticationResult { Succeed = false, Errors = new[] {$"No user with email {email}"} };

            bool valid = await _userManager.CheckPasswordAsync(user, password);

            return valid
                ? new AuthenticationResult { Succeed = true, Token = await GenerateJwtToken(user) }
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
                ? new AuthenticationResult { Succeed = true, Token = await GenerateJwtToken(user) }
                : new AuthenticationResult { Succeed = false, Errors = result.Errors.Select(err => err.Description) };
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsNames.Id, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (string userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role is null) continue;

                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            return claims;
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = await GetUserClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);;
        }

        public async Task<AuthenticationResult> UserNameBelongsToUserAsync(string username, Guid userId)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return new AuthenticationResult {Succeed = false, Errors = new[] {$"No user with username {username}"}};

            return user.Id == userId
                ? new AuthenticationResult {Succeed = true}
                : new AuthenticationResult {Succeed = false, Errors = new[] {$"You are not {username}"}};
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
