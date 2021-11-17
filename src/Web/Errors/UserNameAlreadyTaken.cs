namespace Web.Errors
{
    public readonly struct UserNameAlreadyTaken : IError
    {
        private const string ErrorTemplate = "User name {0} is already taken.";

        public UserNameAlreadyTaken(string userName)
        {
            Message = string.Format(ErrorTemplate, userName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
