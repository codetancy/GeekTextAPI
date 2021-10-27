using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.SqlServer
{
    public class SqlServerCardRepository : ICardRepository
    {
        private readonly AppDbContext _dbContext;

        public SqlServerCardRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Card> GetCardByIdAsync(Guid paymentId)
        {
            return await _dbContext.Cards.SingleOrDefaultAsync(p => p.Id == paymentId);
        }

        public async Task<List<Card>> GetUserCardsAsync(Guid userId)
        {
            return await _dbContext.Cards.Where(p => p.UserId == userId).ToListAsync();
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
