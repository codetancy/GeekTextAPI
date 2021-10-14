using System;

namespace Web.Contracts.V1.Requests
{
    public record RemoveBookFromWishListRequest(Guid CartId, bool AddToCart = false);
}
