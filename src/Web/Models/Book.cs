using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Synopsis { get; set; }
        public decimal UnitPrice { get; set; }
        public int CopiesSold { get; set; }
        public int YearPublished { get; set; }

        public Guid? PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public string GenreName { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Author> Authors { get; set; }
    }
}
