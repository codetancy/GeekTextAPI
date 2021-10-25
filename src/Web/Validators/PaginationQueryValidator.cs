using FluentValidation;
using Web.Contracts.V1.Requests.Queries;

namespace Web.Validators
{
    public class PaginationQueryValidator : AbstractValidator<PaginationQuery>
    {
        public PaginationQueryValidator()
        {
            RuleFor(pagination => pagination.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(pagination => pagination.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100);
        }
    }
}
