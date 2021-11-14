namespace Web.Errors
{
    public interface IError
    {
        string Message { get; }
        int StatusCode { get; }
    }
}
