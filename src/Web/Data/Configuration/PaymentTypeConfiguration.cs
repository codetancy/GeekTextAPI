using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            // Keys
            builder.HasKey(paymentType => paymentType.Name);

            // Properties
            builder.Property(paymentType => paymentType.Name)
                .HasMaxLength(16)
                .IsRequired();

            // Relationships
        }
    }
}
