using DataAccessLayer.Models;

namespace Services.Services;

public class HandleAccountService : IHandleAccountService
{
    private readonly BankAppDataContext _dbContext;

    public HandleAccountService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }


    public ErrorCode CreateTransaction(
        int accountId,
        DateOnly date,
        decimal amount,
        string type,
        string operation
        )
    {
        var accountDb = GetAccount(accountId);

        if (amount >= 0)
        {
            if (amount < 100 || amount > 100000)
            {
                return ErrorCode.IncorrectAmount;
            }
        }

        if (amount <= 0)
        {
            if (amount > -100 || amount < -100000)
            {
                return ErrorCode.IncorrectAmount;
            }

            if (-amount > accountDb.Balance)
            {
                return ErrorCode.BalanceTooLow;
            }
        }


        if (type == string.Empty || type == null)
        {
            return ErrorCode.EmptyType;
        }

        if (operation == string.Empty || operation == null)
        {
            return ErrorCode.EmptyOperation;
        }

        accountDb.Balance += amount;

        var transaction = new Transaction
        {
            AccountId = accountId,
            Date = date,
            Type = type,
            Operation = operation,
            Amount = amount,
            Balance = accountDb.Balance,
        };

        _dbContext.Transactions.Add(transaction);
        _dbContext.SaveChanges();
        return ErrorCode.OK;
    }


    public Account GetAccount(int accountId)
    {
        return _dbContext.Accounts.First(a => a.AccountId == accountId);
    }

}
