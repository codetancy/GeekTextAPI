using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // Keys
            builder.HasKey(userProfile => userProfile.UserId);

            // Properties

            // Relationships
            builder.HasMany(userProfile => userProfile.Payments)
                .WithOne()
                .HasForeignKey(payment => payment.UserId);

            builder.HasMany(userProfile => userProfile.WishLists)
                .WithOne()
                .HasForeignKey(wishlist => wishlist.UserId);

            builder.HasMany(userProfile => userProfile.Carts)
                .WithOne()
                .HasForeignKey(cart => cart.UserId);
        }
    }
}
