namespace Web.Contracts.V1.Responses
{
    public class SingleResponse<T> where T : Response
    {
        public T Data { get; }

        public SingleResponse(T data)
        {
            Data = data;
        }
    }
}
