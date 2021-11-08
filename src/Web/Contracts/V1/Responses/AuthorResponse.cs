using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class AuthorResponse : Response
    {
        public Guid Id { get; init; }
        public string Forename { get; init; }
        public string Surname { get; init; }
        public string PenName { get; init; }
        public string Biography { get; init; }
        public string Publisher { get; init; }
        public IEnumerable<SimpleBookResponse> Books { get; init; }
    }
}
