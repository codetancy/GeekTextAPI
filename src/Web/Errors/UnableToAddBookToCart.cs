using System;

namespace Web.Errors
{
    public readonly struct UnableToAddBookToCart : IError
    {
        private const string ErrorTemplate = "Unable to add book {0} to cart {1}";

        public UnableToAddBookToCart(Guid cartId, Guid bookId)
        {
            Message = string.Format(ErrorTemplate, bookId.ToString(), cartId.ToString());
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
