namespace Web.Errors
{
    public struct RoleAlreadyExists : IError
    {
        private const string ErrorTemplate = "Role {0} already exists.";

        public RoleAlreadyExists(string roleName)
        {
            Message = string.Format(ErrorTemplate, roleName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
