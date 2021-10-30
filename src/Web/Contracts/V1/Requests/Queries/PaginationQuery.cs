namespace Web.Contracts.V1.Requests.Queries
{
    public class PaginationQuery
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
