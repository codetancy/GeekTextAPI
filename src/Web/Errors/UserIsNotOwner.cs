namespace Web.Errors
{
    public readonly struct UserIsNotOwner : IError
    {
        private const string ErrorTemplate = "User does not own the requested {0}.";

        public UserIsNotOwner(string resource)
        {
            Message = string.Format(ErrorTemplate, resource);
        }

        public string Message { get; }
    }
}
