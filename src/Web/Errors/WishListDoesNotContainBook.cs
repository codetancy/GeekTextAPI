using System;

namespace Web.Errors
{
    public readonly struct WishListDoesNotContainBook : IError
    {
        private const string FormatTemplate = "Wishlist {0} does not contain book {1}.";

        public WishListDoesNotContainBook(string wishListName, Guid bookId)
        {
            Message = string.Format(FormatTemplate, wishListName, bookId.ToString());
        }

        public string Message { get; }
    }
}
