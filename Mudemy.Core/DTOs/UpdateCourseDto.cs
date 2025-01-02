namespace Mudemy.Core.DTOs
{
    public class UpdateCourseDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public required string Category { get; set; }
    }
}