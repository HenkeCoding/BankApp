using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;

public class AccountImageModel : PageModel
{
    private readonly ITransactionService _transactionService;
    public AccountImageModel(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }


    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
    }


    public class AccountViewModel
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }


    public List<TransactionViewModel> Transactions { get; set; }

    public void OnGet(int accountId)
    {
        Transactions = _transactionService.GetTransactions(accountId)
            .Select(t => new TransactionViewModel
            {
                TransactionId = t.TransactionId,
                Date = t.Date,
                Type = t.Type,
                Operation = t.Operation,
                Amount = t.Amount,
            })
            .ToList();


    }
}
