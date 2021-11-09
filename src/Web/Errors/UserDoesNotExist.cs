namespace Web.Errors
{
    public readonly struct UserDoesNotExist : IError
    {
        private const string FormatTemplate = "User {0} does not exist.";

        public UserDoesNotExist(string identifier)
        {
            Message = string.Format(FormatTemplate, identifier);
        }

        public string Message { get; }
    }
}
