using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mudemy.Core.Models;

namespace Mudemy.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CardHolderName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CardNumber)
                .IsRequired()
                .HasMaxLength(16)
                .HasConversion<string>();

            builder.Property(p => p.ExpirationDate)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(p => p.CVV)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(p => p.TransactionId)
                .IsRequired()
                .HasMaxLength(36);

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);
        }
    }
}