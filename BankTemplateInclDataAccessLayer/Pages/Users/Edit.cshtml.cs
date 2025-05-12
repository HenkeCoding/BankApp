using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Plugins;
using System.Diagnostics.Metrics;
using BankApp.ViewModels;
using AutoMapper;

namespace BankApp.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly BankAppDataContext _dbContext;
        public EditModel(
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IMapper mapper
            , BankAppDataContext dbContext
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public List<SelectListItem> Roles { get; set; }

        public IdentityUser UserToUpdate { get; set; }

        public IdentityUserViewModel UpdatedUserInfo { get; set; } = new IdentityUserViewModel();

        public string NewUserRole { get; set; }
        public string CurrentUserRole { get; set; }


        public IActionResult OnGet(string userId)
        {
            UserToUpdate = _userManager.FindByIdAsync(userId).Result;

            if (UserToUpdate != null)
            {
                CurrentUserRole = _userManager.GetRolesAsync(UserToUpdate).Result.FirstOrDefault();

                _mapper.Map(UserToUpdate, UpdatedUserInfo);
            }
            else if (UserToUpdate == null)
            {
                return RedirectToPage("Index");
            }

            FillRoleList();
            return Page();
        }

        private void FillRoleList()
        {
            Roles = Enum.GetValues<Role>()
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
                await _userManager.RemoveFromRoleAsync(UserToUpdate, CurrentUserRole);
                await _userManager.AddToRoleAsync(UserToUpdate, NewUserRole);

                _mapper.Map(UpdatedUserInfo, UserToUpdate);

                _dbContext.SaveChanges();
                return RedirectToPage("Index");
            }

            FillRoleList();
            return Page();
        }
    }
}
