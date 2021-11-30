using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Constants;
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
    [Authorize(Roles = RoleNames.User)]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            ICardRepository cardRepository,
            IUserService userService,
            IMapper mapper)
        {
            _cardRepository = cardRepository;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName">User to retrieve</param>
        /// <response code="200">Requested user</response>
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

        /// <summary>Update user information</summary>
        /// <remarks>
        /// 1. User must be logged in.
        /// 2. User can only update its own profile.
        /// </remarks>
        /// <param name="userName">User to update</param>
        /// <param name="request">
        /// User with updated fields.
        /// For full list of fields refer to the UpdateUserRequest schema.
        /// </param>
        /// <inheritdoc cref="UpdateUserRequest"/>
        /// <response code="200">User updated successfully</response>
        /// <response code="400">Invalid fields</response>
        /// <response code="401">User does not own resource</response>
        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser(string userName, UpdateUserRequest request)
        {
            string claimUser = HttpContext.GetUserName();
            if (!userName.Equals(claimUser, StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new UserDoesNotOwnResource(claimUser));

            var updatedUser = _mapper.Map<ApplicationUser>(request);
            var result = await _userService.UpdateUserAsync(userName, updatedUser);
            return result.Match(
                token => Ok(new AuthSucceedResponse(token).ToSingleResponse()),
                err => err.GetResultFromError());
        }

        /// <summary>
        /// Get list of user cards
        /// </summary>
        /// <remarks>
        /// 1. User must be logged in.
        /// 2. User can only see its own cards.
        /// </remarks>
        /// <param name="userName">User to search</param>
        /// <response code="200">List of user cards</response>
        /// <response code="400">Invalid username</response>
        /// <response code="401">User does not own resource</response>
        /// <response code="404">User not found</response>
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

        /// <summary>
        /// Create a card for a user
        /// </summary>
        /// <remarks>
        /// 1) User must be logged in.
        /// 2) User can only add cards to itself.
        /// </remarks>
        /// <param name="userName">User to add a card to</param>
        /// <param name="request">
        /// Card to create.
        /// For full list of fields see CreateCardRequest schema.
        /// </param>
        /// <inheritdoc cref="CreateCardRequest"/>
        /// <response code="200">Card created successfully</response>
        /// <response code="400">Unable to create card</response>
        /// <response code="401">User does not own resource</response>
        /// <response code="404">User/card not found</response>
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

        /// <summary>
        /// Delete a card from a user
        /// </summary>
        /// <remarks>
        /// 1) User must be logged in
        /// 2) User can only delete its own cards
        /// </remarks>
        /// <param name="userName">User to search</param>
        /// <param name="cardId">Id of the card to delete</param>
        /// <response code="204">Card deleted successfully</response>
        /// <response code="400">Unable to delete card</response>
        /// <response code="401">User does not own card</response>
        /// <response code="404">User/card not found</response>
        [HttpDelete("{userName}/cards/{cardId:guid}")]
        public async Task<IActionResult> DeleteUserCard(string userName, Guid cardId)
        {
            string claimUser = HttpContext.GetUserName();
            if (!userName.Equals(claimUser, StringComparison.OrdinalIgnoreCase))
                return Unauthorized(new UserDoesNotOwnResource(claimUser));

            var result = await _cardRepository.DeleteCardByIdAsync(cardId);

            return result.Match(NoContent, error => error.GetResultFromError());
        }
    }
}
