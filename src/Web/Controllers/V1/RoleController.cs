using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Extensions;
using Web.Services.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Gets a list with the name of all roles
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <returns>List of roles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles.ToListedResponse());
        }

        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="request">Object containing the name of the role to create</param>
        /// <response code="200">Successful operations</response>
        /// <response code="400">Role already exist</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var result = await _roleService.CreateRoleAsync(request.RoleName);
            return result.Match(Ok, err => err.GetResultFromError());
        }

        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="roleName">Name of the role to delete</param>
        /// <response code="200">Successful operations</response>
        /// <response code="400">Invalid role provided</response>
        /// <response code="404">Role not found</response>
        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);
            return result.Match(Ok, err => err.GetResultFromError());
        }

        /// <summary>
        /// Assigns a role to a user by its user name.
        /// </summary>
        /// <param name="request">Object containing the user's user name</param>
        /// <param name="roleName">Name of the role to assign</param>
        /// <response code="200">Successful operations</response>
        /// <response code="400">Invalid user name or role name</response>
        /// <response code="404">User or role not found</response>
        [HttpPost("{roleName}/users")]
        public async Task<IActionResult> AddUserToRole(
            [FromRoute] string roleName, [FromBody] AddUserToRoleRequest request)
        {
            var result = await _roleService.AddUserToRole(request.UserName, roleName);
            return result.Match(Ok, error => error.GetResultFromError());
        }

        /// <summary>
        /// Removes a role from a user.
        /// </summary>
        /// <param name="roleName">User's user name</param>
        /// <param name="userName">Name of the role to assign</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid user name or role name</response>
        /// <response code="404">User or role not found</response>
        /// <returns></returns>
        [HttpDelete("{roleName}/users/{userName}")]
        public async Task<IActionResult> RemoveUserFromRole(
            [FromRoute] string roleName, [FromRoute] string userName)
        {
            var result = await _roleService.RemoveUserFromRole(userName, roleName);
            return result.Match(Ok, error => error.GetResultFromError());
        }
    }
}
