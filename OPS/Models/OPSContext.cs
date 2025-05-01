using Microsoft.EntityFrameworkCore;


namespace OPS.Models
{
    public class OPSContext : DbContext
    {
        public OPSContext(DbContextOptions<OPSContext> options)
        : base(options)
        {
        }


        public DbSet<OrderItem> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
