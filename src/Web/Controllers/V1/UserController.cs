using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Extensions;
using Web.Models;
using Web.Repositories.Interfaces;
using Web.Services.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Authorize]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserController(ICardRepository cardRepository, IIdentityService identityService, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string username)
        {
            return await Task.FromResult(Ok());
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser()
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("{username}/cards")]
        public async Task<IActionResult> GetUserCards(string username)
        {
            var userId = HttpContext.GetUserId();
            var result = await _identityService.UsernameBelongsToCurrentUser(username, userId);
            if (!result.Succeed) return BadRequest(result.Errors);

            var cards = await _cardRepository.GetUserCardsAsync(userId);
            var mapping = _mapper.Map<List<Card>, UserCardResponse>(cards);
            var m = _mapper.Map<Card, SimpleCardResponse>(cards.First());

            return Ok(mapping.ToResponse());
        }

        [HttpPost("{username}/cards")]
        public async Task<IActionResult> CreateCard([FromRoute] string username, [FromBody] CreateCardRequest request)
        {
            var userId = HttpContext.GetUserId();
            var result = await _identityService.UsernameBelongsToCurrentUser(username, userId);
            if (!result.Succeed) return BadRequest(result.Errors);

            var card = _mapper.Map<CreateCardRequest, Card>(request);
            card.UserId = userId;
            bool success = await _cardRepository.CreateCardAsync(card);
            if (!success) return BadRequest(new {Error = "Unable to create card."});
            var mapping = _mapper.Map<Card, SimpleCardResponse>(card);

            return CreatedAtAction(nameof(GetUserCards), new { username = username }, mapping.ToResponse());
        }

        [HttpDelete("{username}/cards/{cardId:guid}")]
        public async Task<IActionResult> DeleteUserCard(string username, Guid cardId)
        {
            var userId = HttpContext.GetUserId();
            var result = await _identityService.UsernameBelongsToCurrentUser(username, userId);
            if (!result.Succeed) return BadRequest(result.Errors);

            bool succeed = await _cardRepository.DeleteCardByIdAsync(cardId);
            if (!succeed) return BadRequest(new {Error = "Unable to delete card."});
            return NoContent();
        }
    }
}
