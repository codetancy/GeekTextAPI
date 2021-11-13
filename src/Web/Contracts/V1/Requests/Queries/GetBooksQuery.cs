namespace Web.Contracts.V1.Requests.Queries
{
    public class GetBooksQuery : PaginationQuery
    {
        public string GenreName { get; init; }
        public int? RatingGtEq { get; init; }
    }
}
