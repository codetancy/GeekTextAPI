namespace Web.Errors
{
    public struct UnableToCreate : IError
    {
        private const string ErrorTemplate = "Unable to create {0}. Please try again.";

        public UnableToCreate(string resource)
        {
            Message = string.Format(ErrorTemplate, resource);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
