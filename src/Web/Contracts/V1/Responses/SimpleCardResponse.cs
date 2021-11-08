using System;

namespace Web.Contracts.V1.Responses
{
    public class SimpleCardResponse : Response
    {
        public Guid Id { get; init; }
        public string CardNumber { get; init; }
    }
}
