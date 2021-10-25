namespace Web.Contracts.V1.Requests.Queries
{
    public record PaginationQuery(int PageNumber = 1, int PageSize = 10);
}
