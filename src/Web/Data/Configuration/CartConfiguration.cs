using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            // Keys
            builder.HasKey(cart => cart.CartId);

            // Properties
            builder.Property(cart => cart.Subtotal)
                .HasDefaultValue(0.0m)
                .HasPrecision(6, 2)
                .IsRequired();

            // Relationships
            builder.HasMany(cart => cart.CartBooks)
                .WithOne()
                .HasForeignKey(cartBook => cartBook.CartId);
        }
    }
}
