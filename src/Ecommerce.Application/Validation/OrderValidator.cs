using Ecommerce.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Validation
{
    public class OrderValidator
    {
        public class DeleteOrderCommandValidation : AbstractValidator<OrderCommand.DeleteOrderCommand>
        {
            public DeleteOrderCommandValidation()
            {
                RuleFor(o => o.Id)
                       .NotEmpty().WithMessage("Shopper Id is required")
                       .NotNull().WithMessage("Shopper Id cannot be null");
            }
        }
        public class UpdateOrderCommandValidation : AbstractValidator<OrderCommand.UpdateOrderCommand>
        {
            public UpdateOrderCommandValidation()
            {
                RuleFor(o => o.OrderId)
                   .NotNull().WithMessage("Order Id cannot be null");
            }
        }
    }
}
