using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class CartResponse
    {
        public Guid CartId { get; set; }
        public List<CartBookResponse> CartBooks { get; set; }
    }
}
