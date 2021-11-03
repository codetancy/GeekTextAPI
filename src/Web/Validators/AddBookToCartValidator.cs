using FluentValidation;
using Web.Contracts.V1.Requests;

namespace Web.Validators
{
    public class AddBookToCartValidator : AbstractValidator<AddBookToCartRequest>
    {
        public AddBookToCartValidator()
        {
            RuleFor(a => a.BookId)
                .NotNull();

            RuleFor(a => a.CartId)
                .NotNull();
                
            RuleFor(a => a.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}