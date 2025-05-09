using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using BankApp.ViewModels;
using Azure;

namespace BankApp.Pages.AccountDB;

[Authorize(Roles = "Cashier, Admin")]
public class AccountImageModel : PageModel
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;
    private readonly ICardService _cardService;
    private readonly IDispositionService _dispositionService;
    private readonly ICustomerService _customerService;
    public AccountImageModel(ITransactionService transactionService, 
        IAccountService accountService, 
        ICardService cardService, 
        IDispositionService dispositionService,
        ICustomerService customerService
        )
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _cardService = cardService;
        _dispositionService = dispositionService;
        _customerService = customerService;
    }


    public int AccountId { get; set; }
    public int OwnerCustomerId { get; set; }
    public CustomerViewModel OwnerCustomer { get; set; }

    public int DisponentCustomerId { get; set; }
    public CustomerViewModel DisponentCustomer { get; set; }


    public int PageSize { get; set; }


    public List<TransactionViewModel> Transactions { get; set; }
    public List<CardViewModel> Cards { get; set; }

    public void OnGet(int accountId, int pageSize)
    {
        var account = _accountService.GetAccount(accountId);


        var dispositions = _dispositionService.GetDispositionsByAccountId(accountId);


        foreach (var disposition in dispositions)
        {
            if (disposition.Type == "OWNER")
            {
                OwnerCustomerId = disposition.CustomerId;

                var OwnerCustomerdb = _customerService.GetCustomer(disposition.CustomerId);
                OwnerCustomer = new CustomerViewModel
                {
                    Givenname = OwnerCustomerdb.Givenname,
                    Surname = OwnerCustomerdb.Surname,
                };

            }
            else if (disposition.Type == "DISPONENT")
            {
                DisponentCustomerId = disposition.CustomerId;

                var DisponentCustomerdb = _customerService.GetCustomer(disposition.CustomerId);
                DisponentCustomer = new CustomerViewModel
                {
                    Givenname = DisponentCustomerdb.Givenname,
                    Surname = DisponentCustomerdb.Surname,
                };
            }
        }


        AccountId = accountId;


        if (pageSize == 0)
            pageSize = 10;
        PageSize = pageSize;


        Cards = _cardService.GetCardsByAccountId(accountId)
            .Select(c => new CardViewModel
            {
                CardId = c.CardId,
                Type = c.Type,
                Issued = c.Issued,
                Cctype = c.Cctype,
                Ccnumber = c.Ccnumber,
                Cvv2 = c.Cvv2,
                ExpM = c.ExpM,
                ExpY = c.ExpY,
            }
            )
            .ToList();


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
