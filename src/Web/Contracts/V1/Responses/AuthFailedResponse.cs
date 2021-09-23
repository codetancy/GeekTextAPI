using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public record AuthFailedResponse(List<string> Errors);
}
