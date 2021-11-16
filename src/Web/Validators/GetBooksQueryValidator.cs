using FluentValidation;
using Web.Contracts.V1.Requests.Queries;

namespace Web.Validators
{
    public class GetBooksQueryValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksQueryValidator()
        {
            RuleFor(gbq => gbq.RatingGtEq)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(5);
        }
    }
}
