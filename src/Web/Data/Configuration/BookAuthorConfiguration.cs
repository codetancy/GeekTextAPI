using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Models;

namespace Web.Data.Configuration
{
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            // Keys
            builder.HasKey(bookAuthor => new {bookAuthor.BookId, bookAuthor.AuthorId});

            // Properties

            // Relationships
        }
    }
}
