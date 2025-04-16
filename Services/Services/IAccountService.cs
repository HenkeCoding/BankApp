using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;

public interface IAccountService
{
    PagedResult<Account> GetAccounts(
        int pageNo,
        int pageSize
        );
    void Update(Account account);
    Account GetAccount(int accountId);
}
