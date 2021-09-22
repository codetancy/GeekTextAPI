using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web.Data.Identities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
