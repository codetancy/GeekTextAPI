using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;
using Web.Repositories.Locals;

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

        // GET api/v1/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            return Ok(books);
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
