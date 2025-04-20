using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;
public class AccountService : IAccountService
{
    private readonly BankAppDataContext _dbContext;
    public AccountService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PagedResult<Account> GetAccounts(
        int pageNo,
        int pageSize
        )
    {
        IEnumerable<Account> result = _dbContext.Accounts;
        return result.GetPaged(pageNo, pageSize);
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

