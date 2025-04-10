using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages.Customer;

[Authorize(Roles = "Cashier, Admin")]





public class IndexModel(IAccountService accountService) : PageModel
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

    }

    public void OnGet()
    {



    }
}
