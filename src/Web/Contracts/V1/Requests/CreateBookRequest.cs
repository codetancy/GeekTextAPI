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
        public string PublicationDate { get; init; }
        public string Genre { get; init; }
        public string Publisher { get; init; }
        public decimal Rating { get; init; }
        public List<Guid> AuthorsIds { get; init; }
    }


}
