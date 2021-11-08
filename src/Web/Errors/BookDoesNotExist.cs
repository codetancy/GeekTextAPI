using System;

namespace Web.Errors
{
    public readonly struct BookDoesNotExist : IError
    {
        private const string ErrorTemplate = "Book {0} does not exist.";

        public BookDoesNotExist(Guid bookId)
        {
            Message = string.Format(ErrorTemplate, bookId.ToString());
        }

        public string Message { get; }
    }
}
