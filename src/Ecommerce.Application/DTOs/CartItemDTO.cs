namespace Ecommerce.Application.DTOs
{

    public class CartItemDTO
    {

        public Guid CartItemId { get; set; }


        public string? ProductName { get; set; }


        public Guid ShopperId { get; set; }


        public Guid OrderPrimaryId { get; set; }
    }
}
