using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankApp.Pages.Users;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    public IndexModel()
    {
    }


    public void OnGet()
    {
    }
}
