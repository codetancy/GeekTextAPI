using System;

namespace Web.Models
{
    public class Card
    {
        public Guid PaymentId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int SecurityCode { get; set; }
    }
}
