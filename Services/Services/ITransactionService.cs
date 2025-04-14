using DataAccessLayer.Models;

namespace Services.Services;

public interface ITransactionService
{
    List<Transaction> GetTransactions(int accountId);
    Transaction GetTransaction(int transactionId);
}
