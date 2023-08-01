using Ecommerce.Domain.Models;
using MediatR;

namespace Ecommerce.Application.Commands
{
    public class CheckoutCommand
    {
        public class CheckoutOrderCommand : IRequest
        {
            public CheckoutModel Checkout { get; set; }
        }
    }
}
