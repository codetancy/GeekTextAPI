namespace Web.Contracts.V1.Requests.Queries
{
    public class PaginationQuery
    {
        /// <summary>
        /// Page number. Set to 1 by default.
        /// </summary>
        public int PageNumber { get; init; } = 1;

        /// <summary>
        /// Page size. Set to 10 by default.
        /// </summary>
        public int PageSize { get; init; } = 10;
    }
}
