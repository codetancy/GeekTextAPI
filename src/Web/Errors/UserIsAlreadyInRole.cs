namespace Web.Errors
{
    public readonly struct UserIsAlreadyInRole : IError
    {
        private const string ErrorTemplate = "User {0} is already in role {1}.";

        public UserIsAlreadyInRole(string userName, string roleName)
        {
            Message = string.Format(ErrorTemplate, userName, roleName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
