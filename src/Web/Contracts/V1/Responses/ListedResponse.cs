using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class ListedResponse<T> where T : Response
    {
        public IEnumerable<T> Data { get; init; }

        public ListedResponse(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
