using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web.Data.Identities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
