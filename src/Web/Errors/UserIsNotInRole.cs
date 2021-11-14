namespace Web.Errors
{
    public readonly struct UserIsNotInRole : IError
    {
        private const string ErrorTemplate = "User {0} is not in role {1}.";

        public UserIsNotInRole(string userName, string roleName)
        {
            Message = string.Format(ErrorTemplate, userName, roleName);
        }

        public string Message { get; }
        public int StatusCode => 404;
    }
}
