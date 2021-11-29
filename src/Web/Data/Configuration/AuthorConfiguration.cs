using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            // Keys
            builder.HasKey(author => author.Id);

            // Properties
            builder.Property(author => author.Surname)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(author => author.Forename)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(author => author.PenName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(author => author.Biography)
                .HasMaxLength(1024);

            builder.Property(author => author.Publisher)
                .HasMaxLength(64);

            // Relationships
            builder.HasMany(author => author.BookAuthors)
                .WithOne(bookAuthor => bookAuthor.Author)
                .HasForeignKey(bookAuthor => bookAuthor.AuthorId);
        }
    }
}
