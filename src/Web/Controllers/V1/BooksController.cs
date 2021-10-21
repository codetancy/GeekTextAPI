using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/v1/books?genreName
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                var books = await _bookRepository.GetBooksAsync();
                return Ok(books);
            }
            else
            {
                var books = await _bookRepository.GetBooksByGenreAsync(genreName);
                if (books?.Any() ?? false) return NotFound(new { Error = "Given genre does not exist." });
                return Ok(books);
            }
        }

        // GET ap1/v1/books/{bookId}
        [HttpGet("{bookId:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book is null) return NotFound(new { Error = $"Book {bookId} does not exist" });
            return Ok(book);
        }

        // POST api/v1/books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        {
            if (!string.IsNullOrEmpty(request.Isbn))
            {
                bool exists = await _bookRepository.IsbnExistsAsync(request.Isbn);
                if (exists) return BadRequest(new { Error = $"Book with Isbn {request.Isbn} already exists." });
            }

            var newBook = new Book
            {
                Title = request.Title,
                Isbn = request.Isbn,
                Synopsis = request.Synopsis,
                UnitPrice = request.UnitPrice,
                YearPublished = request.YearPublished,
                PublisherId = request.PublisherId == Guid.Empty ? null : request.PublisherId
            };

            bool success = await _bookRepository.CreateBookAsync(newBook);
            if (!success) return BadRequest(new { Error = "Unable to create book." });

            return CreatedAtAction(nameof(GetBookById), new { bookId = newBook.Id }, newBook);
        }

        // DELETE api/v1/books/{bookId}
        [HttpDelete("{bookId:guid}")]
        public async Task<IActionResult> RemoveBook([FromRoute] Guid bookId)
        {
            bool exists = await _bookRepository.BookExistsAsync(bookId);
            if (!exists) return NotFound(new { Error = $"Book {bookId} does not exist." });

            bool deleted = await _bookRepository.DeleteBookAsync(bookId);
            if (!deleted) return BadRequest(new { Error = "Unable to delete book." });
            return NoContent();
        }

        // PUT api/v1/books/{bookId}
        [HttpPut("{bookId:guid}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
        {
            return await Task.FromResult(Ok());
        }
    }
}
