using DataAccessLayer.Models;

namespace Services.Services;

public class DispositionService : IDispositionService
{
    private readonly BankAppDataContext _dbContext;
    public DispositionService()
    {
        _dbContext = new BankAppDataContext();
    }



    public IEnumerable<Account> GetAccountsByCustomerId(int customerId)
    {
        return GetDispositionsByCustomerId(customerId).Select(c => c.Account).ToList();
    }

    public IEnumerable<Disposition> GetDispositionsByCustomerId(int customerId)
    {
        return _dbContext.Dispositions.Where(c => c.CustomerId == customerId).ToList();
    }

    public IEnumerable<Disposition> GetDispositionsByAccountId(int accountId)
    {
        return _dbContext.Dispositions.Where(c => c.AccountId == accountId).ToList();
    }

    public Account GetAccount(int accountId)
    {
        return _dbContext.Accounts.First(a => a.AccountId == accountId);
    }
}
