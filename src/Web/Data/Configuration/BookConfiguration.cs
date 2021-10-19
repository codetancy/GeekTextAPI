using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Keys
            builder.HasKey(book => book.Id);

            // Properties
            builder.Property(book => book.Title)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(book => book.Isbn)
                .HasMaxLength(17);

            builder.Property(book => book.Synopsis)
                .HasMaxLength(256);

            builder.Property(book => book.UnitPrice)
                .HasDefaultValue(0.0m)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.Property(book => book.YearPublished)
                .IsRequired();

            // Relationships
            builder.HasOne(book => book.Publisher)
                .WithMany()
                .HasForeignKey(book => book.PublisherId);

            builder.HasOne(book => book.Genre)
                .WithMany()
                .HasForeignKey(book => book.GenreName);

            builder.HasMany(book => book.Authors)
                .WithMany(author => author.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    j => j.HasOne<Author>().WithMany().OnDelete(DeleteBehavior.ClientCascade),
                    j => j.HasOne<Book>().WithMany().OnDelete(DeleteBehavior.ClientCascade));
        }
    }
}
