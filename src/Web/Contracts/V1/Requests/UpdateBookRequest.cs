using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Requests
{
    public record UpdateBookRequest(
        string Synopsis, decimal UnitPrice,
        Guid PublisherId, List<Guid> AuthorsIds);
}
