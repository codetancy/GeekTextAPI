using System.Collections.Generic;

namespace Web.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public string Synopsis { get; set; }

        public decimal Price { get; set; }

        public Genre Genre { get; set; }

        public int Copies{ get; set; }

        public string Publisher { get; set; }

        public int Yearpublished { get; set; }

        public List<Author> Authors { get; set; }
    }
}
