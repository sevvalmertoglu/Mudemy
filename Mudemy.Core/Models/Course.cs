using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mudemy.Core.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public Decimal Price { get; set; }
        public string? Description { get; set; }
        public string Category { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}