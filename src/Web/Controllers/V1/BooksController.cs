using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET api/v1/books?genreName=Action
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string genreName)
        {
            List<Book> books;

            if (string.IsNullOrEmpty(genreName))
                books = await _bookRepository.GetBooksAsync();
            else
                books = await _bookRepository.GetBooksByGenreAsync(genreName);

            return books == null || books.Count == 0 ? NotFound("Given genre does not exist."): Ok(books);
        }

        // GET api/v1/books/2
        [HttpGet("{bookId:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);

            if (book is null)
                return BadRequest();

            return Ok(book);
        }
    }
}
