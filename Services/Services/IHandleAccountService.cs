using DataAccessLayer.Models;

namespace Services.Services;

public enum ErrorCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    EmptyComment,
    CommentTooShort,
    CommentTooLong
}

public interface IHandleAccountService
{
    Account GetAccount(int accountId);
    ErrorCode Withdraw(int accountId, decimal amount);
    ErrorCode Deposit(int accountId, decimal amount, string comment);

}
