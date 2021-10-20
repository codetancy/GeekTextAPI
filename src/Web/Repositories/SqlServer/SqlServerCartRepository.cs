using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerCartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
        private readonly List<Cart> _cart;

        public SqlServerCartRepository(AppDbContext dbContext, IBookRepository bookRepository)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
            return await _dbContext.Carts.SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<bool> CreateCartForUserAsync(Guid userId, Cart cart)
        {
            int userCarts = await _dbContext.Carts.Where(c => c.UserId == userId).CountAsync();
            if (userCarts > 1)
                return false;

            await _dbContext.Carts.AddAsync(cart);
            int added = await _dbContext.SaveChangesAsync();
            return added > 0;
        }

        public async Task<List<CartBook>> AddBookToCart(Guid cartId, Guid bookId, int quantity = 1)
        {
            /*
             * TODO: Christian - Implement adding a book to a cart
             * First, get the cart, if it doesn't exist return null
             * Then, see if the book is already in the list of Books by its Id
             * If yes, add the passed quantity to the cartBook quantity
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * If not, then get the book with _bookRepository.GetBookByIdAsync(), if it doesn't exist return null
             * Then, create a new CartBook with the quantity and book
             * Then, add the cartbook to the cart
             * Then, update the cart with _dbContext.Carts.Update()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return the created wishlist
             */

            var cart = _cart.SingleOrDefault(c => cartId == c.CartId);
            if(cart == null){
                return null;
            }

            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if(book == null){
                return null;
            }

            bool cartContainsBook = cart.CartBooks.Any(b => b.Book.Id == bookId);
            if(cartContainsBook){
                var cartBook = cart.CartBooks.Single(cb => cb.Book.Id == bookId);
                cartBook.Quantity += quantity;
            }
            else{
                cart.CartBooks.Add(new CartBook { Book = new Book { Id = bookId } });
            }

            return await Task.FromResult(cart.CartBooks.ToList());

        }

        public async Task<List<CartBook>> RemoveBookFromCart(Guid cartId, Guid bookId)
        {
            /*
             * TODO: Christian - Implement removing a book from a cart
             * First, get the cart, if it doesn't exist, return null
             * Then, see if the book is in the list of Books by its Id, if it doesn't exist, return null
             * Else, remove the book from the list by its Id
             * Then, update the cart with _dbContext.Carts.Update()
             * Then, save your changes with _dbContext.SaveChangesAsync()
             * Lastly return the created wishlist
             */

            var cart = _cart.SingleOrDefault(c => cartId == c.CartId);
            if(cart == null){
                return null;
            }

            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if(book == null){
                return null;
            }

            bool cartContainsBook = cart.CartBooks.Any(b => b.Book.Id == bookId);
            if(cartContainsBook){
                var cartBook = cart.CartBooks.Single(cb => cb.Book.Id == bookId);
                //How to remove cartBook? lol
                //cartBook;
            }
            else{
                cart.CartBooks.Add(new CartBook { Book = new Book { Id = bookId } });
            }

            return await Task.FromResult(cart.CartBooks.ToList());

            throw new NotImplementedException();
        }
    }
}
