using System;
using System.Collections.Generic;

namespace Mudemy.Core.DTOs
{
    public class CreateOrderDto
    {
        public List<int> CourseIds { get; set; } = new List<int>();
    }
}
