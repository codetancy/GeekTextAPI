using System;
using System.Linq;
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

        public static IActionResult GetResultFromError(this IError error) =>
            error.StatusCode switch
            {
                StatusCodes.Status404NotFound => new NotFoundObjectResult(error),
                _ => new BadRequestObjectResult(error)
            };
    }
}
