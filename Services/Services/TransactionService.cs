using DataAccessLayer.Models;

namespace Services.Services;

public class TransactionService : ITransactionService
{
    private readonly BankAppDataContext _dbContext;

    public TransactionService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Transaction> GetTransactions(int accountId)
    {
        return _dbContext.Transactions
            .Where(t => t.AccountId == accountId)
            .ToList();
    }

    public Transaction GetTransaction(int transactionId)
    {
        return _dbContext.Transactions
            .FirstOrDefault(t => t.TransactionId == transactionId);
    }
}
