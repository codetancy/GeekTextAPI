using System;

namespace Web.Contracts.V1.Responses
{
    public class SimpleCardResponse : Response
    {
        public Guid Id { get; init; }
        public string CardNumber { get; init; }
        public string CardHolderName { get; init; }
        public string ExpirationDate { get; init; }
    }
}
