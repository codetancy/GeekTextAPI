using System;
using System.Collections.Generic;

namespace Web.Contracts.V1.Requests
{
    public record CreateWishListRequest(string WishListName, List<Guid> BookIds);
}
