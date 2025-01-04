using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudemy.Core.Models;

namespace Mudemy.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(od => od.Id);

            builder.Property(od => od.OrderId).IsRequired();

            builder.Property(od => od.CourseId).IsRequired();

            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(od => od.Course)
                   .WithMany()
                   .HasForeignKey(od => od.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}