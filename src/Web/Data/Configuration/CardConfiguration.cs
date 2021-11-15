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
                .HasColumnType("char(2)")
                .IsRequired();

            builder.Property(card => card.ExpirationYear)
                .HasColumnType("char(4)")
                .IsRequired();

            builder.Property(card => card.SecurityCode)
                .HasColumnType("char(3)")
                .IsRequired();

            // Constraints
            builder.HasCheckConstraint("CK_Cards_ExpirationMonth",
                $"[{nameof(Card.ExpirationMonth)}] LIKE '[0][1-9]' OR [{nameof(Card.ExpirationMonth)}] LIKE '[1][0-2]'");

            builder.HasCheckConstraint("CK_Cards_ExpirationYear",
                $"[{nameof(Card.ExpirationYear)}] LIKE '[2]%[0-9]%'");

            builder.HasCheckConstraint("CK_Cards_SecurityCode",
                $"[{nameof(Card.SecurityCode)}] LIKE '%[0-9]%'");

            // Relationships
        }
    }
}
