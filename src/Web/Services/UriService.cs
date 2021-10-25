using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using Web.Services.Interfaces;

namespace Web.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginatedUri(int pageNumber, int pageSize)
        {
            string uri = QueryHelpers.AddQueryString(_baseUri, new List<KeyValuePair<string, string>>
            {
                new("pageNumber", pageNumber.ToString()),
                new("pageSize", pageSize.ToString())
            });

            return new Uri(uri);
        }
    }
}
