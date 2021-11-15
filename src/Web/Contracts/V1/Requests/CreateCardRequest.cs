namespace Web.Contracts.V1.Requests
{
    public record CreateCardRequest(
        string CardNumber,
        string CardHolderName,
        string ExpirationMonth,
        string ExpirationYear,
        string SecurityCode);
}
