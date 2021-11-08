using System.Collections.Generic;

namespace Web.Contracts.V1.Responses
{
    public class GenreNamesResponse : Response
    {
        public List<string> Genres { get; init; }
    }
}
