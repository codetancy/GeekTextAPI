using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Requests
{
    public record CreateBookRequest(
        string Title, string Isbn, string Synopsis,
        decimal UnitPrice, int YearPublished, string GenreName,
        Guid PublisherId, List<Guid> AuthorsIds);
}
