using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string PenName { get; set; }
        public string Biography { get; set; }
        public Guid? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
