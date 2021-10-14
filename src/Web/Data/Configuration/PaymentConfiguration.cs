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

            // Properties

            // Relationships
            builder.HasOne(payment => payment.Card)
                .WithOne()
                .HasForeignKey<Card>(card => card.PaymentId);

            builder.HasOne(payment => payment.Type)
                .WithMany()
                .HasForeignKey(payment => payment.TypeName);
        }
    }
}
