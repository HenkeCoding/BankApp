using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankTemplateInclDataAccessLayer.Pages.Account
{
    [Authorize(Roles = "Cashier, Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
