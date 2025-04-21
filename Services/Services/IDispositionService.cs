using DataAccessLayer.Models;

namespace Services.Services;

public interface IDispositionService
{
    IEnumerable<Account> GetAccountsByCustomerId(int customerId);
    IEnumerable<Disposition> GetDispositionsByCustomerId(int customerId);
    Account GetAccount(int accountId);
    IEnumerable<Disposition> GetDispositionsByAccountId(int accountId);
}
