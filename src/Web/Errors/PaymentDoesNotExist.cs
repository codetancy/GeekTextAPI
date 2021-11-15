using System;

namespace Web.Errors
{
    public struct PaymentDoesNotExist : IError
    {
        private const string ErrorTemplate = "Payment {0} does not exist.";

        public PaymentDoesNotExist(Guid paymentId)
        {
            Message = string.Format(ErrorTemplate, paymentId.ToString());
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
