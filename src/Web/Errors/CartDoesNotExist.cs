using System;

namespace Web.Errors
{
    public readonly struct CartDoesNotExist : IError
    {
        private const string ErrorTemplate = "Cart {0} does not exist";

        public CartDoesNotExist(Guid cartId)
        {
            Message = string.Format(ErrorTemplate, cartId);
        }

        public string Message { get; }
        public int StatusCode => 404;
    }
}
