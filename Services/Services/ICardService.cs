using DataAccessLayer.Models;

namespace Services.Services
{
    public interface ICardService
    {
        IEnumerable<Card> GetCardsByAccountId(int accountId);
    }
}