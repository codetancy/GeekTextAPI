using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Requests.Queries;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Controllers.V1
{
    [Authorize(Roles = RoleNames.User)]
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

        /// <summary>
        /// Gets a list of authors
        /// </summary>
        /// <param name="query">Pagination query parameter object</param>
        /// <response code="200">List of authors</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors([FromQuery] PaginationQuery query)
        {
            var filter = _mapper.Map<PaginationFilter>(query);
            var authors = await _authorRepository.GetAllAuthorsAsync(filter);
            var mapping = _mapper.Map<List<AuthorResponse>>(authors);

            return Ok(mapping.ToPagedResponse(_uriService, query.PageNumber, query.PageSize));
        }

        /// <summary>
        /// Gets an author by Id
        /// </summary>
        /// <param name="authorId">Author to retrieve</param>
        /// <response code="200">Requested author</response>
        /// <response code="400">Invalid Id provided</response>
        /// <response code="401">Unauthenticated user</response>
        /// <response code="403">Unauthorized user</response>
        /// <response code="404">Author not found</response>
        [HttpGet("{authorId:guid}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] Guid authorId)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(authorId);

            if (author is null) return NotFound(new { Error = $"Author {authorId} does not exist" });

            var mapping = _mapper.Map<AuthorResponse>(author);
            return Ok(mapping.ToSingleResponse());
        }

        /// <summary>
        /// Creates an author
        /// </summary>
        /// <remarks>Only available to users in the Admin role</remarks>
        /// <param name="request">Author to create</param>
        /// <response code="200">Author created</response>
        /// <response code="400">Invalid fields</response>
        /// <response code="401">Unauthenticated user</response>
        /// <response code="403">Unauthorized user</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest request)
        {
            var author = _mapper.Map<CreateAuthorRequest, Author>(request);

            bool success = await _authorRepository.CreateAuthorAsync(author);
            if (!success) return BadRequest(new {Error = "Unable to create author"});

            var mapping = _mapper.Map<AuthorResponse>(author);
            return CreatedAtAction(nameof(GetAuthorById), new {authorId = mapping.Id}, mapping.ToSingleResponse());
        }

        /// <summary>
        /// Deletes an author
        /// </summary>
        /// <remarks>Only available to users in the Admin role</remarks>
        /// <param name="authorId">Author to remove</param>
        /// <response code="200">Author deleted</response>
        /// <response code="400">Invalid author Id</response>
        /// <response code="401">Unauthenticated user</response>
        /// <response code="403">Unauthorized user</response>
        [Authorize(Roles = RoleNames.Admin)]
        [HttpDelete("{authorId:guid}")]
        public async Task<IActionResult> RemoveAuthor([FromRoute] Guid authorId)
        {
            bool deleted = await _authorRepository.DeleteAuthorAsync(authorId);
            if (!deleted) return BadRequest(new { Error = "Unable to delete author." });
            return NoContent();
        }
    }
}
