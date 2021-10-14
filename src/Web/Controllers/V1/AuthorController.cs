using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
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
            return author is null ? NotFound() : Ok(author);
        }

        // POST api/v1/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            return Ok();
        }

        // PUT api/v1/authors/{authorId}
        [HttpPut("{authorId:guid}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] Guid authorId, [FromBody] UpdateAuthorRequest request)
        {
            return Ok();
        }

        // DELETE api/v1/authors/{authorId}
        [HttpDelete("{authorId:guid}")]
        public async Task<IActionResult> RemoveAuthor([FromRoute] Guid authorId)
        {
            return Ok();
        }

    }
}
