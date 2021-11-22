namespace Web.Errors
{
    public struct WishListAlreadyExists : IError
    {
        private const string ErrorTemplate = "Wishlist {0} already exists";

        public WishListAlreadyExists(string wishListName)
        {
            Message = string.Format(ErrorTemplate, wishListName);
        }

        public string Message { get; }
        public int StatusCode => 400;
    }
}
