using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Keys
            builder.HasKey(payment => payment.Id);
            builder.HasDiscriminator(payment => payment.PaymentType);

            // Properties
            builder.Property(payment => payment.PaymentType)
                .HasColumnName(nameof(Payment.PaymentType))
                .HasMaxLength(16)
                .IsRequired();

            // Relationships
        }
    }
}
