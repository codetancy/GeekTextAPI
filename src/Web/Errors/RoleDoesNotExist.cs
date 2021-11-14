namespace Web.Errors
{
    public struct RoleDoesNotExist : IError
    {
        private const string ErrorTemplate = "Role {0} does not exist";

        public RoleDoesNotExist(string roleName)
        {
            Message = string.Format(ErrorTemplate, roleName);
        }

        public string Message { get; }
        public int StatusCode => 404;
    }
}
