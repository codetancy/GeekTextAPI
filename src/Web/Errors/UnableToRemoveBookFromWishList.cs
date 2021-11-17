using System;

namespace Web.Errors
{
    public readonly struct UnableToRemoveBookFromWishList : IError
    {
        private const string ErroTemplate = "Unable to remove book {0} from wishlist {1}";

        public UnableToRemoveBookFromWishList(string wishListName, Guid bookId)
        {
            Message = string.Format(ErroTemplate, bookId.ToString(), wishListName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
