using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class PagedResponse<T> where T : class
    {
        public IEnumerable<T> Data { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string PreviousPage { get; }
        public string NextPage { get; }

        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, string previousPage, string nextPage)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            PreviousPage = previousPage;
            NextPage = nextPage;
        }

    }

}
