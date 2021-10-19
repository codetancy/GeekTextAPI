using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class WishListBookConfiguration : IEntityTypeConfiguration<WishListBook>
    {
        public void Configure(EntityTypeBuilder<WishListBook> builder)
        {
            // Keys
            builder.HasKey(wishListBook => new
            {
                WishListName = wishListBook.WishListName, BookId = wishListBook.BookId
            });

            // Properties

            // Relationships
            builder.HasOne(wishListBook => wishListBook.Book)
                .WithMany()
                .HasForeignKey(wishListBook => wishListBook.BookId);
        }
    }
}
