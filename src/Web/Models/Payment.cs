using System;

namespace Web.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string TypeName { get; set; }
        public PaymentType Type { get; set; }

        public Card Card { get; set; }
    }
}
