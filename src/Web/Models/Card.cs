using System;

namespace Web.Models
{
    public class Card : Payment
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecurityCode { get; set; }
    }
}
