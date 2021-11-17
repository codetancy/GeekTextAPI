using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class UserReponse : Response
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public List<string> Roles { get; init; }
    }
}
