using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/genres")]
    [Authorize(Roles = RoleNames.Admin)]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;

        public GenreController(IMapper mapper, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Gets a list of all available genres
        /// </summary>
        /// <response code="200">List of all genres</response>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllGenresAsync();
            var mapping = _mapper.Map<List<string>>(genres);
            return Ok(mapping.ToListedResponse());
        }

        /// <summary>
        /// Creates genre
        /// </summary>
        /// <param name="request">Name of the genre</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Genre already exists</response>
        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            bool exists = await _genreRepository.GenreExistsAsync(request.Name);
            if (exists) return BadRequest(new {Error = "Genre already exists"});

            var genre = _mapper.Map<CreateGenreRequest, Genre>(request);
            bool succeed = await _genreRepository.CreateGenreAsync(genre);
            if (!succeed) return BadRequest(new {Error = "Unable to create genre."});

            var mapping = _mapper.Map<Genre, GenreResponse>(genre);
            return CreatedAtAction(nameof(GetAllGenres), mapping.ToSingleResponse());
        }
    }
}
