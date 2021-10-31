using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public int CopiesSold { get; set; }
        public int YearPublished { get; set; }
        public IEnumerable<SimpleAuthorResponse> Authors { get; set; }
    }
}
