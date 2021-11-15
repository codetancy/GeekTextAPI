using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data.Identities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
        public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<WishList> WishLists { get; set; } = new List<WishList>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
