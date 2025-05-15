using BankApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Pages.CustomerDB;

[ResponseCache(Duration = 30, VaryByQueryKeys = ["customerId"])]
[Authorize(Roles = "Cashier, Admin")]
public class CustomerImageModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IDispositionService _dispositionService;
    private readonly ICustomerBalanceService _customerBalanceService;
    private readonly IMapper _mapper;


    public CustomerImageModel(
        ICustomerService customerService, 
        IDispositionService dispositionService, 
        IMapper mapper,
        ICustomerBalanceService customerBalanceService
        )
    {
        _customerService = customerService;
        _dispositionService = dispositionService;
        _customerBalanceService = customerBalanceService;
        _mapper = mapper;

    }
    public CustomerViewModel CustomerToView { get; set; }

    public List<DispositionViewModel> Dispositions { get; set; }

    public List<AccountViewModel> Accounts { get; set; }



    public IActionResult OnGet(int customerId)
    {
        if (customerId == null)
        {
            return RedirectToPage("Index", TempData["ErrorMessage"] = $"Invalid ID.");
        }

        var CustomerDb = _customerService.GetCustomer(customerId);

        if (CustomerDb.CustomerId == 0)
        {
            return RedirectToPage("Index", TempData["ErrorMessage"] = $"No customer was found with that ID.");
        }

        CustomerToView = new CustomerViewModel();

        _mapper.Map(CustomerDb, CustomerToView);

        CustomerToView.ViewId = CustomerDb.CustomerId;

        CustomerToView.TotalBalance = _customerBalanceService.GetCustomerTotalBalanceByCustomerId(customerId);


        Dispositions = _dispositionService.GetDispositionsByCustomerId(customerId)
            .Select(d => new DispositionViewModel
            {
                DispositionId = d.DispositionId,
                CustomerId = d.CustomerId,
                AccountId = d.AccountId,
                Type = d.Type,


                Account = (new AccountViewModel
                {
                    AccountId = d.AccountId,
                    Frequency = _dispositionService.GetAccount(d.AccountId).Frequency,
                    Balance = _dispositionService.GetAccount(d.AccountId).Balance,
                    Created = _dispositionService.GetAccount(d.AccountId).Created,
                })



            }).ToList();

        

        Accounts = _dispositionService.GetAccountsByCustomerId(customerId)
            .Select(a => new AccountViewModel
            {
                AccountId = a.AccountId,
                Frequency = a.Frequency,
                Balance = a.Balance,
                Created = a.Created,
            })
            .ToList();


        return Page();
    }
}
