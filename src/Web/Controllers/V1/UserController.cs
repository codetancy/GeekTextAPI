using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Contracts.V1.Responses;
using Web.Data.Identities;
using Web.Errors;
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
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserController(
            ICardRepository cardRepository,
            IUserService userService,
            IIdentityService identityService,
            IMapper mapper)
        {
            _cardRepository = cardRepository;
            _userService = userService;
            _identityService = identityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName">User to retrieve</param>
        /// <response code="200">Successful operation</response>
        /// <response code="404">User not found</response>
        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string userName)
        {
            var result = await _userService.GetUserAsync(userName);
            return result.Match(
                res => Ok(_mapper.Map<UserReponse>(res).ToSingleResponse()),
                err => err.GetResultFromError());
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser()
        {
            return await Task.FromResult(Ok());
        }

        [HttpGet("{userName}/cards")]
        public async Task<IActionResult> GetUserCards(string userName)
        {
            string claimUser = HttpContext.GetUserName();
            if (!userName.Equals(claimUser, StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new UserDoesNotOwnResource(claimUser));

            var userId = HttpContext.GetUserId();
            var result = await _cardRepository.GetUserCardsAsync(userId);

            return result.Match(
                cards =>
                {
                    var mapping = _mapper.Map<List<SimpleCardResponse>>(cards);
                    var response = new UserCardResponse(userName, mapping);
                    return Ok(response.ToSingleResponse());
                },
                error => error.GetResultFromError());
        }

        [HttpPost("{userName}/cards")]
        public async Task<IActionResult> CreateCard(string userName, [FromBody] CreateCardRequest request)
        {
            string claimUser = HttpContext.GetUserName();
            if (!userName.Equals(claimUser, StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new UserDoesNotOwnResource(claimUser));

            var card = _mapper.Map<Card>(request);
            card.UserId = HttpContext.GetUserId();
            var result = await _cardRepository.CreateCardAsync(card);

            return result.Match(Ok, error => error.GetResultFromError());
        }

        [HttpDelete("{userName}/cards/{cardId:guid}")]
        public async Task<IActionResult> DeleteUserCard(string userName, Guid cardId)
        {
            var userId = HttpContext.GetUserId();
            var result = await _identityService.UserNameBelongsToUserAsync(userName, userId);
            if (!result.Succeed) return BadRequest(result.Errors);

            bool succeed = await _cardRepository.DeleteCardByIdAsync(cardId);
            if (!succeed) return BadRequest(new {Error = "Unable to delete card."});
            return NoContent();
        }
    }
}
