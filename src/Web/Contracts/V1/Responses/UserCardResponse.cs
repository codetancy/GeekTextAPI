using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class UserCardResponse : Response
    {
        public UserCardResponse()
        {
        }

        public UserCardResponse(string userName, IEnumerable<SimpleCardResponse> cards)
        {
            UserName = userName;
            Cards = cards;
        }

        public string UserName { get; init; }
        public IEnumerable<SimpleCardResponse> Cards { get; init; }
    }
}
