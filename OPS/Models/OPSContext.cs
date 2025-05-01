using Microsoft.EntityFrameworkCore;

namespace OPS.Models
{
    public class OPSContext : DbContext
    {
        public OPSContext(DbContextOptions<OPSContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }  // Correct DbSet for Orders
        public DbSet<Product> Products { get; set; }  // Correct DbSet for Products
        public DbSet<OrderItem> OrderItems { get; set; }  // DbSet for OrderItems
    }
}
