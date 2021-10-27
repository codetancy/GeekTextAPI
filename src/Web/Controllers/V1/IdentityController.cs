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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            (string email, string password) = request;
            var result = await _identityService.LoginAsync(email, password);
            return result.Succeed
                ? Ok(new AuthSucceedResponse(result.Token))
                : BadRequest(new AuthFailedResponse(result.Errors.ToList()));
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            (string email, string userName, string password) = request;
            var result = await _identityService.SignupAsync(email, userName, password);
            return result.Succeed
                ? Ok(new AuthSucceedResponse(result.Token))
                : BadRequest(new AuthFailedResponse(result.Errors.ToList()));
        }
    }
}
