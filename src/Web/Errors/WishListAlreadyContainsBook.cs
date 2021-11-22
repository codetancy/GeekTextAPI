using System;

namespace Web.Errors
{
    public struct WishListAlreadyContainsBook : IError
    {
        private const string ErrorTemplate = "Wishlist {0} already contains book {1}";

        public WishListAlreadyContainsBook(string wishListName, Guid bookId)
        {
            Message = string.Format(ErrorTemplate, wishListName, bookId);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
