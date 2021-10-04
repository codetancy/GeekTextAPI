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

        // GET api/v1/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET api/v1/authors/{AuthorID}
        [HttpGet("{authorId:int}")]
        public async Task<IActionResult> GetBookById([FromRoute] int authorId)
        {
            var author = await _authorRepository.GetAuthorsByIdAsync(authorId);

            if (author is null)
                return BadRequest();

            return Ok(author);
        }
    }
}
