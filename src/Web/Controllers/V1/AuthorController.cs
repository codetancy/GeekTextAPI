using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET api/v1/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET api/v1/authors/{authorId}
        [HttpGet("{authorId:guid}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] Guid authorId)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(authorId);
            if (author is null) return NotFound(new { Error = $"Author {authorId} does not exist" });
            return Ok(author);
        }

        // POST api/v1/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            var author = new Author
            {
                Forename = request.Forename,
                Surname = request.Surname,
                PenName = request.PenName,
                Biography = request.Biography
            };

            bool success = await _authorRepository.CreateAuthorAsync(author);
            if (!success) return BadRequest(new { Error = "Unable to create author" });
            return Ok(author);
        }

        // PUT api/v1/authors/{authorId}
        [HttpPut("{authorId:guid}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] Guid authorId, [FromBody] UpdateAuthorRequest request)
        {
            return await Task.FromResult(Ok());
        }

        // DELETE api/v1/authors/{authorId}
        [HttpDelete("{authorId:guid}")]
        public async Task<IActionResult> RemoveAuthor([FromRoute] Guid authorId)
        {
            bool deleted = await _authorRepository.DeleteAuthorAsync(authorId);
            if (!deleted) return BadRequest(new { Error = "Unable to delete author." });
            return NoContent();
        }
    }
}
