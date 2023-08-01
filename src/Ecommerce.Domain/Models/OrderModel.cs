using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enumeration;

namespace Ecommerce.Domain.Models
{
    
    public class OrderModel
    {
       
        public Guid OrderId { get; set; }
        
        public Guid UserPrimaryId { get; set; }
        
        public OrderStatus OrderStatus { get; set; }
        
        public ICollection<CartItemEntity> CartItemEntities { get; set; } = new List<CartItemEntity>();
        
    }
}
