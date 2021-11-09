namespace Web.Contracts.V1.Responses
{
    public class LoggedUserResponse : Response
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
