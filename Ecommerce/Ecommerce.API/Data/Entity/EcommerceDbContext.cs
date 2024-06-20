using Ecommerce.API.Data.Entity.DbSet;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data.Entity;
public class EcommerceDbContext: DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
          : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}

