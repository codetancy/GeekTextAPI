namespace Web.Errors
{
    public struct UserDoesNotOwnResource : IError
    {
        private const string ErrorTemplate = "User {0} does not own the requested resource.";

        public UserDoesNotOwnResource(string userName)
        {
            Message = string.Format(ErrorTemplate, userName);
        }

        public string Message { get; }
        public int StatusCode => 401;
    }
}
