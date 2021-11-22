using System;

namespace Web.Errors
{
    public struct UnableToAddBookToWishList : IError
    {
        private const string ErrorTemplate = "Unable to add book {0} to wishlist {1}";
        public UnableToAddBookToWishList(string wishListName, Guid bookId)
        {
            Message = string.Format(ErrorTemplate, bookId, wishListName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
