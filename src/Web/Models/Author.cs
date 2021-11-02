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
        public string Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
