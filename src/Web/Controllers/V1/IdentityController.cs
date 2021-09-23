using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Services.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return await Task.FromResult(Ok());
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            (string email, string userName, string password) = request;
            var result = await _identityService.SignupAsync(email, userName, password);

            if (result.Succeed)
            {
                var succeedResponse = new AuthSucceedResponse(result.Token);
                return Ok(succeedResponse);
            }

            var failedResponse = new AuthFailedResponse(result.Errors.ToList());
            return BadRequest(failedResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            return await Task.FromResult(Ok());
        }
    }
}
