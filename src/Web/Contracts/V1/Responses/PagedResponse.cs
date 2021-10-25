using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public record PagedResponse<T>(IEnumerable<T> Data, int PageNumber, int PageSize, string PreviousPage,
        string NextPage) where T : class;
}
