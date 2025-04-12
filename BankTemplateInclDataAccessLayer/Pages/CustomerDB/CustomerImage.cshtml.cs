using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using DataAccessLayer.Models;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB
{
    public class CustomerImageModel : PageModel
    {
        private readonly ICustomerService _customerService;
        CustomerImageModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public Customer customer { get; set; }

        public void OnGet()
        {
        }
    }
}
