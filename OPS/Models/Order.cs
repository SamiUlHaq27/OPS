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

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();  // Navigation property
    }
}
