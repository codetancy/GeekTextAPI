using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface ICardRepository
    {
        Task<Card> GetCardByIdAsync(Guid paymentId);
        Task<Result<List<Card>>> GetUserCardsAsync(Guid userId);
        Task<Result> CreateCardAsync(Card card);
        Task<Result> DeleteCardByIdAsync(Guid paymentId);
    }
}
