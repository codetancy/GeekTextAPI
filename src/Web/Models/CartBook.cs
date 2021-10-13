using System;

namespace Web.Models
{
    public class CartBook
    {
        public Guid CartId { get; set; }

        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
