using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Keys
            builder.HasKey(genre => genre.Name);

            // Properties
            builder.Property(genre => genre.Name)
                .HasMaxLength(16)
                .IsRequired();

            // Relationships
        }
    }
}
