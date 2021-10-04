using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class WishList
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Book> Books { get; set; }
    }
}
