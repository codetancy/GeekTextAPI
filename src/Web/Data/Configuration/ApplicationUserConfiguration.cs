using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Data.Identities;
using Web.Models;

namespace Web.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Keys

            // Properties

            // Relationships
            builder.HasMany(user => user.UserRoles)
                .WithOne(userRole => userRole.User)
                .HasForeignKey(userRole => userRole.UserId)
                .IsRequired();

            builder.HasMany(user => user.Payments)
                .WithOne()
                .HasForeignKey(payment => payment.UserId);

            builder.HasMany(user => user.WishLists)
                .WithOne()
                .HasForeignKey(wishlist => wishlist.UserId);

            builder.HasMany(user => user.Carts)
                .WithOne()
                .HasForeignKey(cart => cart.UserId);
        }
    }
}
