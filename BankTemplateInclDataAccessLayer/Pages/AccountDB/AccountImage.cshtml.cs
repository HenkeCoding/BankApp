using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using BankTemplateInclDataAccessLayer.ViewModels;

namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;

[Authorize(Roles = "Cashier, Admin")]
public class AccountImageModel : PageModel
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;
    public AccountImageModel(ITransactionService transactionService, IAccountService accountService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
    }

    public int AccountId { get; set; }
    public int CustomerId { get; set; }
    public int PageSize { get; set; } = 5;


    public List<TransactionViewModel> Transactions { get; set; }


    public void OnGet(int accountId, int pageSize)
    {
        var account = _accountService.GetAccount(accountId);

        AccountId = accountId;
        PageSize = pageSize;
    }


    public IActionResult OnGetShowMore(int accountId, int pageNo, int pageSize)
    {
        var transactionsUnhandled = _transactionService.GetTransactionsByAccountId(accountId, pageNo, pageSize);


        Transactions = transactionsUnhandled.Results
            .Select(c => new TransactionViewModel
            {
                TransactionId = c.TransactionId,
                Date = c.Date,
                Type = c.Type,
                Operation = c.Operation,
                Amount = c.Amount,
                Balance = c.Balance,
                Symbol = c.Symbol,
                Bank = c.Bank
            }
            )
            .ToList();


        return new JsonResult(new { listOfTransactions = Transactions });
    }
}
