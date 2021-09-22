using System;
using Microsoft.AspNetCore.Identity;

namespace Web.Data.Identities
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}
