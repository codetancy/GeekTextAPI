using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class BookResponse : Response
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Synopsis { get; set; }
        public decimal UnitPrice { get; set; }
        public int CopiesSold { get; set; }
        public string PublicationDate { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal Rating { get; set; }
        public IEnumerable<SimpleAuthorResponse> Authors { get; set; }
    }
}
