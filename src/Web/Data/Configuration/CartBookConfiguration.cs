using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class CartBookConfiguration : IEntityTypeConfiguration<CartBook>
    {
        public void Configure(EntityTypeBuilder<CartBook> builder)
        {
            // Keys
            builder.HasKey(cartBook => new {CartId = cartBook.CartId, BookId = cartBook.BookId});

            // Properties
            builder.Property(cartBook => cartBook.Quantity)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(cartBook => cartBook.Price)
                .HasDefaultValue(0.0m)
                .HasPrecision(6, 2)
                .IsRequired();

            // Relationships
            builder.HasOne(cartBook => cartBook.Book)
                .WithMany()
                .HasForeignKey(cartBook => cartBook.BookId);
        }
    }
}
