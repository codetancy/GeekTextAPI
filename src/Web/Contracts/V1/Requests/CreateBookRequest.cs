using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Requests
{
    public class CreateBookRequest
    {
        public string Title { get; init; }
        public string Isbn { get; init; }
        public string Synopsis { get; init; }
        public decimal UnitPrice { get; init; }
        public int CopiesSold { get; init; }
        public int YearPublished { get; init; }
        public string GenreName { get; init; }
        public string Publisher { get; init; }
        public List<Guid> AuthorsIds { get; init; }
    }


}
