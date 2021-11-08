using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v1/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IGenreRepository _genreRepository;

        public GenreController(IMapper mapper, IUriService uriService, IGenreRepository genreRepository)
        {
            _mapper = mapper;
            _uriService = uriService;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGenres([FromQuery] PaginationQuery query)
        {
            var filter = _mapper.Map<PaginationQuery, PaginationFilter>(query);
            var genres = await _genreRepository.GetAllGenresAsync(filter);
            var mapping = _mapper.Map<List<Genre>, GenreNamesResponse>(genres);
            return Ok(mapping.Genres.ToPagedResponse(_uriService, filter.PageNumber, filter.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            bool exists = await _genreRepository.GenreExistsAsync(request.Name);
            if (exists) return BadRequest(new {Error = "Genre already exists"});

            var genre = _mapper.Map<CreateGenreRequest, Genre>(request);
            bool succeed = await _genreRepository.CreateGenreAsync(genre);
            if (!succeed) return BadRequest(new {Error = "Unable to delete genre."});

            var mapping = _mapper.Map<Genre, GenreResponse>(genre);
            return CreatedAtAction(nameof(GetAllGenres), mapping.ToSingleResponse());
        }
    }
}
