using System;

namespace Web.Errors
{
    public readonly struct UserDoesNotExist : IError
    {
        private const string FormatTemplate1 = "User with Id {0} does not exist";
        private const string FormatTemplate2 = "User {0} does not exist.";

        public UserDoesNotExist(Guid userId)
        {
            Message = string.Format(FormatTemplate1, userId.ToString());
        }

        public UserDoesNotExist(string identifier)
        {
            Message = string.Format(FormatTemplate2, identifier);
        }

        public string Message { get; }
        public int StatusCode => 404;
    }
}
