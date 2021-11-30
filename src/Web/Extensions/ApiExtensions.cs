using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Errors;

namespace Web.Extensions
{
    public static class ApiExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            string potentialId = httpContext.User.Claims
                .SingleOrDefault(claim => claim.Type == ClaimsNames.Id)?.Value;
            return Guid.TryParse(potentialId, out Guid userId) ? userId : Guid.Empty;
        }

        public static string GetUserName(this HttpContext httpContext)
        {
            var claims = httpContext.User.Claims;
            return claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static IActionResult GetResultFromError(this IError error) =>
            error.StatusCode switch
            {
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(error),
                StatusCodes.Status404NotFound => new NotFoundObjectResult(error),
                _ => new BadRequestObjectResult(error)
            };
    }
}
