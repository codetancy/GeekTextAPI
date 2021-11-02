using System;

namespace Web.Contracts.V1.Requests
{
    public class CreateAuthorRequest
    {
        public string Forename { get; init; }
        public string Surname { get; init; }
        public string PenName { get; init; }
        public string Biography { get; init; }
        public string Publisher { get; init; }
    }
}
