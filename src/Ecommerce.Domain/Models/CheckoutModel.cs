using Ecommerce.Domain.Enumeration;
using System.Text.Json.Serialization;

namespace Ecommerce.Domain.Models
{
    
    public class CheckoutModel
    {
        
        public Guid OrderPrimaryId { get; set; }
        
        public OrderStatus OrderStatus { get; set; }
        
    }
}
