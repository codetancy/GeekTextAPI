using System;
using System.Collections.Generic;
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
                return books == null || books.Count == 0 ? NotFound("Given genre does not exist."): Ok(books);
            }
        }

        // GET ap1/v1/books/{bookId}
        [HttpGet("{bookId:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            return book is null ? NotFound() : Ok(book);
        }

        // POST api/v1/books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        {
            var book = new Book
            {
                Title = request.Title,
                Isbn = request.Isbn,
                Synopsis = request.Synopsis,
                UnitPrice = request.UnitPrice,
                YearPublished = request.YearPublished
            };

            bool success = await _bookRepository.CreateBookAsync(book);

            return success ? Ok(book) : BadRequest();
        }

        // DELETE api/v1/books/{bookId}
        [HttpDelete("{bookId:guid}")]
        public async Task<IActionResult> RemoveBook([FromRoute] Guid bookId)
        {
            bool deleted = await _bookRepository.DeleteBookAsync(bookId);
            return deleted ? NoContent() : BadRequest();
        }

        // PUT api/v1/books/{bookId}
        [HttpPut("{bookId:guid}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest request)
        {
            return Ok();
        }
    }
}
