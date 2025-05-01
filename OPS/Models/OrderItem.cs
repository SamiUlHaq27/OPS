using System.ComponentModel.DataAnnotations;

namespace OPS.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }  // Foreign Key
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        // Navigation property to Product
        public Product Product { get; set; }
    }
}
