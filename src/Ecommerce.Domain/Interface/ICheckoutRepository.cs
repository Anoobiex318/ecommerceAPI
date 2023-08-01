using Ecommerce.Domain.Models;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interface
{
    
    public interface ICheckoutRepository
    {
        
        Task<Guid> CheckoutOrderEntity(CheckoutEntity checkout);
       
    }
}
