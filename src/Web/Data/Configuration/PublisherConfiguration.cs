using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            // Keys
            builder.HasKey(publisher => publisher.Id);

            // Properties
            builder.Property(publisher => publisher.Name)
                .HasMaxLength(32)
                .IsRequired();

            // Relationships
            builder.HasMany(publisher => publisher.Authors)
                .WithOne(author => author.Publisher)
                .HasForeignKey(author => author.PublisherId);
        }
    }
}
