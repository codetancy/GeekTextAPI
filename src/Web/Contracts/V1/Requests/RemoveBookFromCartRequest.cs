using System;

namespace Web.Contracts.V1.Requests
{
    public record RemoveBookFromCartRequest(Guid CartId);
}
