using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{authorId:guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid authorId)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(authorId);

            if (author is null)
                return BadRequest();

            return Ok(author);
        }
    }
}
