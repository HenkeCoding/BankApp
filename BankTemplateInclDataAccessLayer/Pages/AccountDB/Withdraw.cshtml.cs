using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Pages.AccountDB;

[BindProperties]
[Authorize(Roles = "Cashier, Admin")]
public class WithdrawModel : PageModel
{
    private readonly IHandleAccountService _handleAccountService;

    public WithdrawModel(IHandleAccountService handleAccountService)
    {
        _handleAccountService = handleAccountService;
    }

    [Range(100, 10000)]
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }


    public void OnGet(int accountId)
    {
        Balance = _handleAccountService.GetAccount(accountId).Balance;
    }

    public IActionResult OnPost(int accountId)
    {
        var status = _handleAccountService.Withdraw(accountId, Amount);

        if (ModelState.IsValid)
        {
            if (status == ErrorCode.OK)
            {
                return RedirectToPage("Index");
            }
        }

        else if (status == ErrorCode.BalanceTooLow)
        {
            ModelState.AddModelError("Amount", "Insufficient money to withdraw.");
        }
        else if (status == ErrorCode.IncorrectAmount)
        {
            ModelState.AddModelError("Amount", "The amount must be between 100 and 10000.");
        }

        return Page();
    }


}
