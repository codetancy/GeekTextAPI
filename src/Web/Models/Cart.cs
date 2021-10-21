using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public decimal Subtotal { get; set; }

        public ICollection<CartBook> CartBooks { get; set; } = new List<CartBook>();
    }
}
