using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public record BookResponse(
        Guid Id, string Title, string Isbn, decimal Price,
        string Genre, string Publisher, List<SimpleAuthorResponse> Authors);
}
