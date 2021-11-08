namespace Web.Errors
{
    public readonly struct UnableToDelete : IError
    {
        public string Message => "Unable to delete resource. Please try again.";
    }
}
