using System;

namespace Web.Models
{
    public class WishListBook
    {
        public string WishListName { get; set; }
        public WishList WishList { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public WishListBook()
        {

        }

        public WishListBook(string wishListName, Guid bookId)
        {

            WishListName = wishListName;
            BookId = bookId;
        }
    }
}
