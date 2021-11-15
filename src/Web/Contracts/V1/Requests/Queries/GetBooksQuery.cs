namespace Web.Contracts.V1.Requests.Queries
{
    public class GetBooksQuery : PaginationQuery
    {
        /// <summary>
        /// Name of genre to filter by (optional).
        /// </summary>
        public string GenreName { get; init; }

        /// <summary>
        /// Minimum book rating. Must be an integer between 0 and 5, inclusive (optional).
        /// </summary>
        public int? RatingGtEq { get; init; }
    }
}
