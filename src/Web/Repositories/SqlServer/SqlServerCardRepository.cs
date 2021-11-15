using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.Identities;
using Web.Errors;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerCardRepository : ICardRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public SqlServerCardRepository(
            AppDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<Card> GetCardByIdAsync(Guid paymentId)
        {
            return await _dbContext.Cards.SingleOrDefaultAsync(p => p.Id == paymentId);
        }

        private async Task<List<Card>> GetCardsAsync(Guid userId)
            => await _dbContext.Cards.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();

        public async Task<Result<List<Card>>> GetUserCardsAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return new Result<List<Card>>(new UserDoesNotExist(userId));

            var cards = await GetCardsAsync(userId);
            return new Result<List<Card>>(cards);
        }

        public async Task<Result<List<Card>>> GetUserCardsAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                return new Result<List<Card>>(new UserDoesNotExist(userName));

            var cards = await GetCardsAsync(user.Id);
            return new Result<List<Card>>(cards);
        }

        public async Task<bool> CreateCardAsync(Card card)
        {
            await _dbContext.Cards.AddAsync(card);
            int added = await _dbContext.SaveChangesAsync();
            return added > 0;
        }

        public async Task<bool> DeleteCardByIdAsync(Guid paymentId)
        {
            var card = await _dbContext.Cards.SingleOrDefaultAsync(p => p.Id == paymentId);
            if (card is null) return false;

            _dbContext.Cards.Remove(card);
            int deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
