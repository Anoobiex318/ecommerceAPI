using Ecommerce.Domain.Enumeration;

namespace Ecommerce.Application.DTOs
{
    public class UpdateOrderDTO
    {
        public OrderStatus orderStatus { get; set; }
    }
}
