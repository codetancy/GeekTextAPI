using System;

namespace Web.Contracts.V1.Requests
{
    public record CreateAuthorRequest(
        string Forename, string Surname, string PenName,
        string Biography, Guid PublisherId);
}
