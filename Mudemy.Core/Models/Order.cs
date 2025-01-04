using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudemy.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required string UserId { get; set; } 
        public UserApp User { get; set; } = default!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public int CourseId { get; set; } // Foreign Key
        public Course Course { get; set; } = default!; // Navigation Property
    }
}