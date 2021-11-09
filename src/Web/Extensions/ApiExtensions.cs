using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Web.Extensions
{
    public static class ApiExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            string potentialId = httpContext.User.Claims.SingleOrDefault(claim => claim.Type == "Id")?.Value;
            return Guid.TryParse(potentialId, out Guid userId) ? userId : Guid.Empty;
        }
    }
}
