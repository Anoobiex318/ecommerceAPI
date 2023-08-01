namespace Ecommerce.Domain.Models
{
    
    public class CartItemModel
    {
        
        public Guid CartItemId { get; set; }
        
        public Guid ShopperId { get; set; }
        
        public Guid OrderPrimaryId { get; set; }
        
        public string? ProductName { get; set; }
        
    }
}
