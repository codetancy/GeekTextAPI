namespace Web.Errors
{
    public readonly struct UnableToUpdate : IError
    {
        private const string ErrorTemplate = "Unable to update {0}";

        public UnableToUpdate(string resource)
        {
            Message = string.Format(ErrorTemplate, resource);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
