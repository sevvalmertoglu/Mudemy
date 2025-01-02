using System;
using System.Collections.Generic;

namespace Mudemy.Core.DTOs
{
    public class CreateOrderDto
    {
        public required string UserId { get; set; }
        public List<int> CourseIds { get; set; } = new List<int>();
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
