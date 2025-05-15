using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Services.Services;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models;
using Services.Infrastructure.Validation;

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

    public int AccountId { get; set; }

    public decimal Balance { get; set; }

    [Range(100, 100000)]
    public decimal Amount { get; set; }

    public List<SelectListItem> Types { get; set; }

    [ValidTypeAttribute]
    public string Type { get; set; }

    public string Operation { get; set; }


    public void OnGet(int accountId)
    {
        AccountId = accountId;
        Balance = _handleAccountService.GetAccount(accountId).Balance;
        FillTypeList();
    }

    private void FillTypeList()
    {
        Types = Enum.GetValues<DataAccessLayer.Models.Type>()
            .Select(g => new SelectListItem
            {
                Value = g.ToString(),
                Text = g.ToString()
            }).ToList();
    }

    // Anropas vid POST-begäran
    public IActionResult OnPost(int accountId)
    {
        var depositDate = DateOnly.FromDateTime(DateTime.Now);

        Amount = -Amount; // Negate the amount for withdrawal

        var status = _handleAccountService.CreateTransaction(accountId, depositDate, Amount, Type, Operation);


        if (ModelState.IsValid)
        {
            if (status == ErrorCode.OK)
            {
                return RedirectToPage("Index", TempData["Message"] = $"{Amount} has been successfully withdrawn from Account number {accountId}.");
            }
        }


        else if (status == ErrorCode.EmptyType)
        {
            ModelState.AddModelError("Type", "Can't leave an empty type!");
        }

        else if (status == ErrorCode.EmptyOperation)
        {
            ModelState.AddModelError("Operation", "Can't leave an empty operation!");
        }

        else if (status == ErrorCode.IncorrectAmount)
        {
            ModelState.AddModelError("Amount", "The amount must be between 100 and 10000.");
        }

        else if (status == ErrorCode.BalanceTooLow)
        {
            ModelState.AddModelError("Amount", "The amount exceeds the available balance.");
        }

        Balance = _handleAccountService.GetAccount(accountId).Balance;
        FillTypeList();
        return Page();
    }
}
