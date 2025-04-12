using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using DataAccessLayer.Models;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB
{
    public class CustomerImageModel : PageModel
    {
        private readonly ICustomerService _customerService;
        public CustomerImageModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Customer Customer { get; set; }

        public void OnGet(int customerId)
        {
            Customer = _customerService.GetCustomer(customerId);


        }
    }
}
