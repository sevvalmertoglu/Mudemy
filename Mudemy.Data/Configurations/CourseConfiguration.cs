using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudemy.Core.Models;

namespace Mudemy.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name).IsRequired()             
                   .HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500); 
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)"); 
            builder.Property(x => x.UserId).IsRequired()       
                   .HasMaxLength(200);
            builder.Property(x => x.Category).IsRequired()       
                   .HasMaxLength(100);                          
        }
    }
}
