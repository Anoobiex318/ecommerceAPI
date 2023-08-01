using Ecommerce.Domain.Enumeration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid OrderId { get; set; }
       

        [ForeignKey(nameof(UserEntity))]
        public Guid UserPrimaryId { get; set; }
       

        public UserEntity? UserEntity { get; set; }
       

        public OrderStatus OrderStatus { get; set; }
        

        public ICollection<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
        
    }
}
