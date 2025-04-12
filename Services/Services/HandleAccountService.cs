using DataAccessLayer.Models;

namespace Services.Services;

public class HandleAccountService : IHandleAccountService
{
    private readonly BankAppDataContext _dbContext;

    public HandleAccountService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ErrorCode Withdraw(int accountId, decimal amount)
    {
        var accountDb = _dbContext.Accounts.First(a => a.AccountId == accountId);

        if (accountDb.Balance < amount)
        {
            return ErrorCode.BalanceTooLow;
        }

        if (amount < 100 || amount > 10000)
        {
            return ErrorCode.IncorrectAmount;
        }

        accountDb.Balance -= amount;
        _dbContext.SaveChanges();
        return ErrorCode.OK;
    }
    public ErrorCode Deposit(int accountId, decimal amount, string comment)
    {
        var accountDb = _dbContext.Accounts.First(a => a.AccountId == accountId);

        if (amount < 100 || amount > 10000)
        {
            return ErrorCode.IncorrectAmount;
        }

        if (comment == string.Empty)
        {
            return ErrorCode.EmptyComment;
        }

        if (comment.Length < 5)
        {
            return ErrorCode.CommentTooShort;
        }

        if (comment.Length > 200)
        {
            return ErrorCode.CommentTooLong;
        }

        accountDb.Balance += amount;
        _dbContext.SaveChanges();
        return ErrorCode.OK;
    }

    public Account GetAccount(int accountId)
    {
        return _dbContext.Accounts.First(a => a.AccountId == accountId);
    }

}
