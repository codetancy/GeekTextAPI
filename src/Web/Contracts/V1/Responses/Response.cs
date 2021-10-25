namespace Web.Contracts.V1.Responses
{
    public record Response<T>(T Data) where T : class;
}
