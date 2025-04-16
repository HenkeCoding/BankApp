using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Services.Services;


namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;

[Authorize(Roles = "Cashier, Admin")]

public class IndexModel : PageModel
{
    private readonly IAccountService _accountService;
    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }



    public class AccountViewModel
    {
        public int AccountId { get; set; }

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }

    }
    public IEnumerable<AccountViewModel> Accounts { get; set; }



    public int PageNo { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; } = 5;




    public void OnGet(
        int pageNo,
        int pageSize,
        string q
        )
    {
        if (pageNo == 0)
            pageNo = 1;
        PageNo = pageNo;

        if (pageSize == 0)
            pageSize = 5;
        PageSize = pageSize;

        var result = _accountService.GetAccounts(PageNo, PageSize);

        PageCount = result.PageCount;

        Accounts = result.Results
            .Select(c => new AccountViewModel
            {
                AccountId = c.AccountId,
                Balance = c.Balance,
                Frequency = c.Frequency,
                Created = c.Created
            })
            .ToList();

    }
}
