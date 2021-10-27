using System;

namespace Web.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PaymentType { get; set; }
    }
}
