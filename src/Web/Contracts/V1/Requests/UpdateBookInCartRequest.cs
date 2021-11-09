using System;

namespace Web.Contracts.V1.Requests
{
    public class UpdateBookInCartRequest
    {
        public Guid CartId { get; init; }
        public int Quantity { get; init; }
    }
}