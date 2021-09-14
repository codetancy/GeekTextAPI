using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Synopsis { get; set; }

        public decimal Price { get; set; }

        public List<Author> Authors { get; set; }
    }
}
