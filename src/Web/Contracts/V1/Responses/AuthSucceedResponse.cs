namespace Web.Contracts.V1.Responses
{
    public class AuthSucceedResponse : Response
    {
        public string Token { get; init; }

        public AuthSucceedResponse(string token)
        {
            Token = token;
        }
    }
}
