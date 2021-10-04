using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public double Subtotal { get; set; }
        public List<Book> Books { get; set; }
    }
}