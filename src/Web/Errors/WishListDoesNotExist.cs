namespace Web.Errors
{
    public readonly struct WishListDoesNotExist : IError
    {
        private const string ErrorTemplate = "Wishlist {0} does not exist.";

        public WishListDoesNotExist(string wishListName)
        {
            Message = string.Format(ErrorTemplate, wishListName);
        }

        public string Message { get; }
    }
}
