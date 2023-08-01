using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    public class CartItemEntity
    {
       
        [Key]
        public Guid CartItemId { get; set; }

       
        public string? ProductName { get; set; }

        
        public Guid ShopperId { get; set; }

      
        [ForeignKey(nameof(OrderEntity))]
        public Guid OrderPrimaryId { get; set; }
        public OrderEntity? OrderEntity { get; set; }

    }
}
