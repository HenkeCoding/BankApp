using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Services;
using System.ComponentModel.DataAnnotations;

namespace BankTemplateInclDataAccessLayer.Pages.AccountDB;

[BindProperties]
[Authorize(Roles = "Cashier, Admin")]
public class DepositModel : PageModel
{
    private readonly IHandleAccountService _handleAccountService;
    public DepositModel(IHandleAccountService handleAccountService)
    {
        _handleAccountService = handleAccountService;
    }

    public int AccountId { get; set; }
    [Range(100, 10000)]
    public decimal Amount { get; set; }
    public DateTime DepositDate { get; set; }
    public string Comment { get; set; }

    public void OnGet(int accountId)
    {
        AccountId = accountId;
        DepositDate = DateTime.Now.AddHours(1);
    }

    // Anropas vid POST-begäran
    public IActionResult OnPost(int accountId)
    {
        if (DepositDate < DateTime.Now)
        {
            ModelState.AddModelError(
    "DepositDate", "Cannot Deposit money in the past!");
        }

        var status = _handleAccountService.Deposit(accountId, Amount, Comment);

        if (ModelState.IsValid)
        {
            if (status == ErrorCode.OK)
            {
                return RedirectToPage("Index");
            }
        }

        else if (status == ErrorCode.EmptyComment)
        {
            ModelState.AddModelError("Comment", "Can't leave an empty comment!");
        }

        else if (status == ErrorCode.CommentTooShort)
        {
            ModelState.AddModelError("Comment", "Comments must be at least 5 characters long");
        }

        else if (status == ErrorCode.CommentTooLong)
        {
            ModelState.AddModelError("Comment", "OK, thats just too many words");
        }

        else if (status == ErrorCode.IncorrectAmount)
        {
            ModelState.AddModelError("Amount", "The amount must be between 100 and 10000.");
        }

        return Page();

    }

}


