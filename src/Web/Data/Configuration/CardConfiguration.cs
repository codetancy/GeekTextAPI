using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            // Keys

            // Properties
            builder.Property(card => card.CardNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(card => card.CardHolderName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(card => card.ExpirationMonth)
                .IsRequired();

            builder.Property(card => card.ExpirationYear)
                .IsRequired();

            builder.Property(card => card.SecurityCode)
                .IsRequired();

            // Relationships
        }
    }
}
