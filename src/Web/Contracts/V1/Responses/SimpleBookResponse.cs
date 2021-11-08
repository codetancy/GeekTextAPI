using System;

namespace Web.Contracts.V1.Responses
{
    public class SimpleBookResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; }

        public SimpleBookResponse(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
