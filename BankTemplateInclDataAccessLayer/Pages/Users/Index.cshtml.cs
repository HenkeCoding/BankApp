using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankApp.ViewModels;
using DataAccessLayer.Models;

namespace BankApp.Pages.Users;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public IndexModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public List<IdentityUserViewModel> Users { get; set; }



    public async Task OnGetAsync()
    {
        List<IdentityUser> identityUsers = _userManager.Users.ToList();

        Users = new List<IdentityUserViewModel>();

        foreach (var user in identityUsers)
        {
            // Fetch roles for each user
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

            // Add the user view model to the list
            Users.Add(new IdentityUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.PasswordHash ?? string.Empty, // Handle possible null value
                Roles = roles
            });
        }
    }
}
