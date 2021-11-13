namespace Web.Models
{
    public class BookSearchFilter : PaginationFilter
    {
        public string GenreName { get; set; }
        public int? RatingGtEq { get; set; }
    }
}
