namespace Web.Contracts.V1.Requests
{
    public record CreateCardRequest(
        string CardNumber,
        string CardHolderName,
        int ExpirationMonth,
        int ExpirationYear,
        int SecurityCode);
}
