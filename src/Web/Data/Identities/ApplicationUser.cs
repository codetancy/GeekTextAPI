using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data.Identities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public UserProfile Profile { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
