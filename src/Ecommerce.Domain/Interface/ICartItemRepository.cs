using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interface
{
    
    public interface ICartItemRepository
    {
        
        Task<CartItemEntity> AddCartItem(CartItemEntity newCartItem);
        
        Task<List<CartItemModel>> GetAllCartItems(Guid shopperId);
        
        Task<CartItemEntity> UpdateCartItem(CartItemEntity cartItem);
        
        Task DeleteCartItem(Guid CartItemId);
        
    }
}
