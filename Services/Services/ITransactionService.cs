using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;

public interface ITransactionService
{
    PagedResult<Transaction> GetTransactionsByAccountId(int accountId, int pageNo, int pageSize);
    Transaction GetTransaction(int transactionId);
}
