using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Web.Contracts.V1.Responses;

namespace Web.Extensions
{
    public static class ApiExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            string potentialId = httpContext.User.Claims.Single(claim => claim.Type == "Id").Value;
            return Guid.TryParse(potentialId, out Guid userId) ? userId : Guid.Empty;
        }

        public static Response<T> ToResponse<T>(this T response) where T : class
            => new Response<T>(response);
    }
}
