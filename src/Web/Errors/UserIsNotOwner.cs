namespace Web.Errors
{
    public struct UserIsNotOwner : IError
    {
        public string Message => "User is not the owner of the requested resource.";
    }
}
