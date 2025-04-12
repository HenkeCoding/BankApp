using DataAccessLayer.Models;

namespace Services.Services;

public interface IAccountService
{
    List<Account> GetAccounts();
    void Update(Account account);
    Account GetAccount(int accountId);
}
