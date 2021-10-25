using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // GET api/v1/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            var mapping = _mapper.Map<List<Author>, List<AuthorResponse>>(authors);
            return Ok(mapping.ToResponse());
        }

        // GET api/v1/authors/{authorId}
        [HttpGet("{authorId:guid}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] Guid authorId)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(authorId);

            if (author is null) return NotFound(new { Error = $"Author {authorId} does not exist" });

            var mapping = _mapper.Map<Author, AuthorResponse>(author);
            return Ok(mapping.ToResponse());
        }

        // POST api/v1/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            var author = _mapper.Map<CreateAuthorRequest, Author>(request);

            bool success = await _authorRepository.CreateAuthorAsync(author);
            if (!success) return BadRequest(new { Error = "Unable to create author" });

            var mapping = _mapper.Map<Author, AuthorResponse>(author);
            return CreatedAtAction(nameof(GetAuthorById), new { authorId = mapping.Id }, mapping.ToResponse());
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
