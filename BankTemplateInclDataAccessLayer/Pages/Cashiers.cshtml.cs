using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankTemplateInclDataAccessLayer.Pages
{
    [Authorize(Roles = "Admin")]
    public class CashiersModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
