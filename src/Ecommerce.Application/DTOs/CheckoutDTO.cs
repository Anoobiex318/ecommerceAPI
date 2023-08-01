using Ecommerce.Domain.Enumeration;

namespace Ecommerce.Application.DTOs
{

    public class CheckoutDTO
    {

        public Guid OrderPrimaryId { get; set; }


        public Guid ShopperId { get; set; }


        public OrderStatus OrderStatus { get; set; }
    }
}
