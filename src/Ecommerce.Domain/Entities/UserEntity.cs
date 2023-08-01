using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }

       
        public string? UserName { get; set; }

        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}
