using System.Collections.Generic;

namespace Web.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Succeed { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
