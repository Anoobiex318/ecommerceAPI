using Ecommerce.Application.Commands;
using FluentValidation;

namespace Ecommerce.Application.Validation
{
    public class CheckoutValidation : AbstractValidator<CheckoutCommand.CheckoutOrderCommand>
    {
        public CheckoutValidation()
        {
            RuleFor(co => co.Checkout)
                .NotEmpty().WithMessage("Checkout is required")
                .NotNull().WithMessage("Checkout cannot be null");
        }
    }
}
