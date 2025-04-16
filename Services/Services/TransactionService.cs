using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;

public class TransactionService : ITransactionService
{
    private readonly BankAppDataContext _dbContext;

    public TransactionService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PagedResult<Transaction> GetTransactionsByAccountId(int accountId, int pageNo, int pageSize)
    {
        IEnumerable<Transaction> results = _dbContext.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderBy(t => t.Date);

        return results.GetPaged(pageNo, pageSize);
    }

    public Transaction GetTransaction(int transactionId)
    {
        return _dbContext.Transactions
            .FirstOrDefault(t => t.TransactionId == transactionId);
    }
}
