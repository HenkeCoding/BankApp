using DataAccessLayer.Models;

namespace Services.Services;

public enum ErrorCode
{
    OK,
    BalanceTooLow,
    IncorrectAmount,
    EmptyType,
    EmptyOperation
}

public interface IHandleAccountService
{
    Account GetAccount(int accountId);
    ErrorCode CreateTransaction(
        int accountId,
        DateOnly date,
        decimal amount, 
        string type, 
        string operation);

}
