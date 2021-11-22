namespace Web.Errors
{
    public struct UserReachedMaxNumOfWishLists : IError
    {
        private const string ErrorTemplate = "User has reached maximum number of wishlists";

        public string Message => ErrorTemplate;
        public int StatusCode => 400;
    }
}
