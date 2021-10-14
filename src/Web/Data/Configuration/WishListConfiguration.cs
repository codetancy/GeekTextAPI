using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            // Keys
            builder.HasKey(wishlist => wishlist.Name);

            // Properties
            builder.Property(wishlist => wishlist.Name)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(wishlist => wishlist.Description)
                .HasMaxLength(32);

            // Relationships
            builder.HasMany(wishlist => wishlist.WishListBooks)
                .WithOne(wishListBook => wishListBook.WishList)
                .HasForeignKey(wishListBook => wishListBook.WishListName);
        }
    }
}
