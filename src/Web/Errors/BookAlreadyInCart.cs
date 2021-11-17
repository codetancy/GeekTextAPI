using System;

namespace Web.Errors
{
    public readonly struct BookAlreadyInCart : IError
    {
        private const string ErrorTemplate = "Book {0} is already in cart {1}.";

        public BookAlreadyInCart(Guid bookId, Guid cartId)
        {
            Message = string.Format(ErrorTemplate, bookId, cartId);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
