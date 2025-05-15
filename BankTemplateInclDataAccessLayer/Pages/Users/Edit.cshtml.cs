using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using System.Diagnostics.Metrics;
using BankApp.ViewModels;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Services.Infrastructure.Validation;

namespace BankApp.Pages.Users;

[BindProperties]
public class EditModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly BankAppDataContext _dbContext;
    public EditModel(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper,
        BankAppDataContext dbContext
        )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public List<SelectListItem> RoleList { get; set; }

    // Pseudocode plan:
    // 1. Ensure UserId is set before OnPostDelete is called. 
    // 2. In OnGet, set UserId from the query parameter.
    // 3. In OnPostDelete, check if UserId is null or empty and handle accordingly (e.g., return to Index).
    // 4. Optionally, add [BindProperty] to UserId to ensure model binding on POST.

    public string UserId { get; set; }

    public string Email { get; set; }

    [ValidRoleAttribute]
    public string UserRole { get; set; }


    public IActionResult OnGet(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToPage("Index");
        }

        UserId = userId;
        var userToUpdate = _userManager.FindByIdAsync(userId).Result;

        if (userToUpdate != null)
        {
            UserRole = _userManager.GetRolesAsync(userToUpdate).Result.FirstOrDefault();

            Email = userToUpdate.Email;
        }
        else
        {
            return RedirectToPage("Index");
        }

        FillRoleList();
        return Page();
    }

    private void FillRoleList()
    {
        RoleList = Enum.GetValues<Role>()
            .Select(g => new SelectListItem
            {
                Value = g.ToString(),
                Text = g.ToString()
            }).ToList();
    }

    public async Task<IActionResult> OnPostAsync(string userId)
    {
        if (ModelState.IsValid)
        {
            var userToUpdate = await _userManager.FindByIdAsync(userId);

            if (userToUpdate == null)
            {
                return RedirectToPage("Index");
            }

            var currentUserRoles = await _userManager.GetRolesAsync(userToUpdate);

            foreach (var role in currentUserRoles)
            {
                await _userManager.RemoveFromRoleAsync(userToUpdate, role);
            }

            await _userManager.AddToRoleAsync(userToUpdate, UserRole);

            userToUpdate.Email = Email;
            userToUpdate.UserName = Email;
            userToUpdate.NormalizedEmail = Email.ToUpper();
            userToUpdate.NormalizedUserName = Email.ToUpper();

            _dbContext.SaveChanges();
            ViewData["Message"] = "User successfully edited!";
            FillRoleList();
            return Page();
        }

        FillRoleList();
        return Page();
    }

    public IActionResult OnPostDelete(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return Page();
        }
        var user = _userManager.FindByIdAsync(userId).Result;
        if (user != null)
        {
            _userManager.DeleteAsync(user);
            ViewData["Message"] = "User successfully deleted!";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
