using System;

namespace Web.Contracts.V1.Responses
{
    public record SimpleCardResponse
    {
        public Guid Id { get; init; }
        public string CardNumber { get; init; }
    }
}
