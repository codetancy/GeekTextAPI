namespace Web.Contracts.V1.Responses
{
    public class CartBookResponse : Response
    {
        public SimpleBookResponse Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
