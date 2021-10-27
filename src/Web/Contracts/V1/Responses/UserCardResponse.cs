using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class UserCardResponse
    {
        public Guid UserId { get; init; }
        public IEnumerable<SimpleCardResponse> Cards { get; set; }
    }
}
