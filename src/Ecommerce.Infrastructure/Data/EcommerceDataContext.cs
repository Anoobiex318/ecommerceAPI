using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    public class EcommerceDataContext : DbContext
    {
        public EcommerceDataContext(DbContextOptions<EcommerceDataContext> options) : base(options)
        { 

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CheckoutEntity> Checkouts { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
    }
}
