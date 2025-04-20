using DataAccessLayer.Models;

namespace Services.Services;

public class DispositionService : IDispositionService
{
    private readonly BankAppDataContext _dbContext;
    public DispositionService()
    {
        _dbContext = new BankAppDataContext();
    }

    public IEnumerable<Card> GetCardsByCustomerId(int accountId)
    {
        return _dbContext.Cards.Where(c => c.Disposition.AccountId == accountId).ToList();
    }

    public IEnumerable<Account> GetAccountsByCustomerId(int customerId)
    {
        return _dbContext.Dispositions.Where(c => c.CustomerId == customerId).Select(c => c.Account).ToList();
    }

    public IEnumerable<Disposition> GetDispositionsByCustomerId(int customerId)
    {
        return _dbContext.Dispositions.Where(c => c.CustomerId == customerId).ToList();
    }

    public Account GetAccount(int accountId)
    {
        return _dbContext.Accounts.First(a => a.AccountId == accountId);
    }
}
