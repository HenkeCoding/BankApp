using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB;

public class CustomerImageModel : PageModel
{
    private readonly ICustomerService _customerService;
    public CustomerImageModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public class CustomerViewModel
    {
        public string Givenname { get; set; }
        public string Surname { get; set; }
    }

    public class DispositionViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }

    public CustomerViewModel Customer { get; set; }

    public List<DispositionViewModel> Accounts { get; set; }

    public void OnGet(int customerId)
    {
        var CustomerDb = _customerService.GetCustomer(customerId);

        Customer = new CustomerViewModel
        {
            Givenname = CustomerDb.Givenname,
            Surname = CustomerDb.Surname
            
        };

        Accounts = CustomerDb.Dispositions

        Accounts = new List<AccountViewModel>();
    }
}
