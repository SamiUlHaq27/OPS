using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPS.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach (var item in OrderItems)
                {
                    total += item.Quantity * item.ProductPrice;
                }
                return total;
            }
        }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
