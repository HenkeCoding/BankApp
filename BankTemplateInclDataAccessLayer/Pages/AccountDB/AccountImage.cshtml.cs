using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using Services.Infrastructure.Paging;

namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;


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
    public int PageSize { get; set; } = 5;


    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? Symbol { get; set; }
        public string? Bank { get; set; }
    }


    public class AccountViewModel
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }


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


        return new JsonResult( new { listOfTransactions = Transactions } );
    }
}
