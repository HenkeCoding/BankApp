using BankApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;


namespace BankApp.Pages.AccountDB;

[Authorize(Roles = "Cashier, Admin")]
public class IndexModel : PageModel
{
    private readonly IAccountService _accountService;
    public IndexModel(IAccountService accountService)
    {
        _accountService = accountService;
    }

    
    public List<AccountViewModel> Accounts { get; set; }

    public int PageNo { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }


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
            pageSize = 50;
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
