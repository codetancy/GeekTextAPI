using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class WishList
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public ICollection<WishListBook> WishListBooks { get; set; }
    }
}
