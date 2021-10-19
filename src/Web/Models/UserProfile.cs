using System;
using System.Collections.Generic;
using Web.Data.Identities;

namespace Web.Models
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
