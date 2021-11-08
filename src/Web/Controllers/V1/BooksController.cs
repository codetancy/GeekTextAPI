using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Requests.Queries;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/books")]
    public class BooksController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUriService _uriService;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IAuthorRepository authorRepository, IUriService uriService, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _uriService = uriService;
            _mapper = mapper;
        }

        // GET api/v1/books?genreName
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] GetBooksQuery query)
        {
            var filter = _mapper.Map<GetBooksQuery, BookSearchFilter>(query);
            var books = await _bookRepository.GetBooksAsync(filter);
            var mapping = _mapper.Map<List<Book>, List<BookResponse>>(books);

            return Ok(mapping.ToPagedResponse(_uriService, filter.PageNumber, filter.PageSize));
        }

        // GET api/v1/books/best-selling
        [HttpGet("best-sellers")]
        public async Task<IActionResult> GetTopSellers()
        {
            var books = await _bookRepository.GetTopSellersAsync();
            var mapping = _mapper.Map<List<Book>, List<BookResponse>>(books);
            return Ok(mapping.ToListedResponse());
        }

        // GET ap1/v1/books/{bookId}
        [HttpGet("{bookId:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book is null) return NotFound(new { Error = $"Book {bookId} does not exist" });

            var mapping = _mapper.Map<Book, BookResponse>(book);
            return Ok(mapping.ToSingleResponse());
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

            if (request.AuthorsIds?.Any() ?? false)
            {
                bool duplicates = request.AuthorsIds.Count != request.AuthorsIds.Distinct().Count();
                if (duplicates) return BadRequest("List of authors cannot contain duplicates.");

                bool valid = await _authorRepository.AuthorsExistAsync(request.AuthorsIds);
                if (!valid) return BadRequest("At least one of the authors provided do not exist");
            }

            var newBook = _mapper.Map<CreateBookRequest, Book>(request);
            bool success = await _bookRepository.CreateBookAsync(newBook, request.AuthorsIds);
            if (!success) return BadRequest(new { Error = "Unable to create book." });

            var mapping = _mapper.Map<Book, BookResponse>(newBook);
            return CreatedAtAction(nameof(GetBookById), new { bookId = mapping.Id }, mapping.ToSingleResponse());
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
