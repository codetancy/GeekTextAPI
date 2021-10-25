using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public record AuthorResponse(Guid Id, string Forename, string Surname, string PenName, string Biography,
        string Publisher)
    {
        public IEnumerable<SimpleBookResponse> Books { get; init; }
    }
}
