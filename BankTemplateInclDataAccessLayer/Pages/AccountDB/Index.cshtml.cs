using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;


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

    }


    private readonly IAccountService _accountService;

    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IEnumerable<AccountViewModel> Accounts { get; set; }

    public void OnGet()
    {
        Accounts = _accountService.GetAccounts()
            .Select(x => new AccountViewModel
            {
                AccountId = x.AccountId,
                Balance = x.Balance,
                Frequency = x.Frequency,
                Created = x.Created
            })
            .ToList();

    }
}
