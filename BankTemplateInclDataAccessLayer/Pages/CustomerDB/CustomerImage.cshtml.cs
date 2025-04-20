using BankTemplateInclDataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB;

[Authorize(Roles = "Cashier, Admin")]
public class CustomerImageModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IDispositionService _dispositionService;

    public CustomerImageModel(ICustomerService customerService, IDispositionService dispositionService)
    {
        _customerService = customerService;
        _dispositionService = dispositionService;
    }
    public CustomerViewModel Customer { get; set; }

    public List<DispositionViewModel> Dispositions { get; set; }

    public List<AccountViewModel> Accounts { get; set; }



    public void OnGet(int customerId)
    {
        var CustomerDb = _customerService.GetCustomer(customerId);

        Customer = new CustomerViewModel
        {
            Givenname = CustomerDb.Givenname,
            Surname = CustomerDb.Surname,
            Gender = CustomerDb.Gender,
            Streetaddress = CustomerDb.Streetaddress,
            City = CustomerDb.City,
            Zipcode = CustomerDb.Zipcode,
            Country = CustomerDb.Country,
            CountryCode = CustomerDb.CountryCode,
            Birthday = CustomerDb.Birthday,
            NationalId = CustomerDb.NationalId,
            Telephonecountrycode = CustomerDb.Telephonecountrycode,
            Telephonenumber = CustomerDb.Telephonenumber,
            Emailaddress = CustomerDb.Emailaddress
        };


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



    }
}
