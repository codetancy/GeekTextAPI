using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Web.Contracts.V1.Responses;
using Web.Services.Interfaces;

namespace Web.Extensions
{
    public static class ApiExtensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            string potentialId = httpContext.User.Claims.Single(claim => claim.Type == "Id").Value;
            return Guid.TryParse(potentialId, out Guid userId) ? userId : Guid.Empty;
        }

        public static SingleResponse<T> ToSingleResponse<T>(this T response) where T : Response
            => new SingleResponse<T>(response);

        public static ListedResponse<T> ToListedResponse<T>(this IEnumerable<T> response) where T : Response
            => new ListedResponse<T>(response);

        public static PagedResponse<T> ToPagedResponse<T>(this IEnumerable<T> response,
            IUriService uriService, int pageNumber, int pageSize) where T : Response
        {
            string previousPage = null;
            if (pageNumber > 1)
                previousPage = uriService.GetPaginatedUri(pageNumber - 1, pageSize).ToString();

            string nextPage = null;
            if (response.Count() == pageSize)
                nextPage = uriService.GetPaginatedUri(pageNumber + 1, pageSize).ToString();

            return new PagedResponse<T>(response, pageNumber, pageSize, previousPage, nextPage);
        }
    }
}
