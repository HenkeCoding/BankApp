using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB;

[Authorize(Roles = "Cashier, Admin")]

public class IndexModel : PageModel
{
    private readonly ICustomerService _customerService;
    public IndexModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public List<Customer> Customers { get; set; }

    public class CustomerViewModel
    {
        public int Id { get; set; }
    }

    public void OnGet()
    {
        Customers = _customerService.GetCustomers();
    }
}
