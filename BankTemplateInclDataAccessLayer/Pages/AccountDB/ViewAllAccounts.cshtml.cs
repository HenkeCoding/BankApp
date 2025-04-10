using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using DataAccessLayer.Models;


namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;

[Authorize(Roles = "Cashier, Admin")]

public class IndexModel : PageModel
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; } = new List<Disposition>();

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public virtual ICollection<PermanentOrder> PermanentOrders { get; set; } = new List<PermanentOrder>();

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }


    private readonly IAccountService _accountService;

    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }


    public IEnumerable<Account> Accounts { get; set; } = new List<Account>();


    public void OnGet()
    {
        Accounts = _accountService.GetAccounts()
            .Select(x => new Account
            {
                AccountId = x.AccountId,
                Balance = x.Balance,
                Frequency = x.Frequency,
                Created = x.Created,
                Dispositions = x.Dispositions,
                Loans = x.Loans,
                PermanentOrders = x.PermanentOrders,
                Transactions = x.Transactions
            }).ToList();

    }
}
