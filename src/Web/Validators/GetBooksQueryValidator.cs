namespace Web.Validators
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(gbq => gbq.RatingGtEq)
                .GreaterThanOrEqualToZero(0)
                .LessThanOrEqualTo(5);
        }
    }
}
