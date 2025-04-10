using DataAccessLayer.Models;

namespace Services.Services;
public class AccountService : IAccountService
{
    private readonly BankAppDataContext _dbContext;

    public AccountService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> GetAccounts()
    {
        return _dbContext.Accounts.ToList();
    }

    public Account GetAccount(int accountId)
    {
        return _dbContext.Accounts.First(a => a.AccountId == accountId);
    }

    public void Update(Account account)
    {
        _dbContext.SaveChanges();
    }
}

