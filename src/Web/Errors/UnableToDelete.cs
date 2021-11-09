namespace Web.Errors
{
    public readonly struct UnableToDelete : IError
    {
        private const string FormatTemplate = "Unable to delete {0}. Please try again.";

        public UnableToDelete(string resource)
        {
            Message = string.Format(FormatTemplate, resource);
        }

        public string Message { get; }
    }
}
