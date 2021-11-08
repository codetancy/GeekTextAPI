using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; init; }

        public AuthFailedResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
