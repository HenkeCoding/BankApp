using DataAccessLayer.Models;

namespace Services.Services;

public interface IDispositionService
{
    IEnumerable<Card> GetCardsByCustomerId(int accountId);
    IEnumerable<Account> GetAccountsByCustomerId(int customerId);
    IEnumerable<Disposition> GetDispositionsByCustomerId(int customerId);
    Account GetAccount(int accountId);
}
