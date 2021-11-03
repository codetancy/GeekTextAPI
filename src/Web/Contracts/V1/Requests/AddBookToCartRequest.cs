using System;

namespace Web.Contracts.V1.Requests
{
    public record AddBookToCartRequest(Guid BookId, Guid CartId, int Quantity);
}
