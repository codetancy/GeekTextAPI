using System;
using System.Collections.Generic;
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
    [Route("api/v1/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper, IUriService uriService )
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        // GET api/v1/authors?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors([FromQuery] PaginationQuery query)
        {
            var filter = _mapper.Map<PaginationQuery, PaginationFilter>(query);
            var authors = await _authorRepository.GetAllAuthorsAsync(filter);
            var mapping = _mapper.Map<List<Author>, List<AuthorResponse>>(authors);

            return Ok(mapping.ToPagedResponse(_uriService, query.PageNumber, query.PageSize));
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
