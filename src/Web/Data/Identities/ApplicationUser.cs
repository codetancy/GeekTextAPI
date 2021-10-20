using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data.Identities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
