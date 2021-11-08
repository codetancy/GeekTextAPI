using System;

namespace Web.Contracts.V1.Responses
{
    public class SimpleAuthorResponse
    {
        public Guid Id { get; init; }
        public string PenName { get; init; }

        public SimpleAuthorResponse(Guid id, string penName)
        {
            Id = id;
            PenName = penName;
        }
    }
}
