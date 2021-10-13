using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}
