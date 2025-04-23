using AutoMapper;
using BankApp.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Services;

namespace BankApp.Pages.CustomerDB;

[BindProperties]
[Authorize(Roles = "Cashier, Admin")]
public class CreateNewModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    public CreateNewModel(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }


    public int CustomerId { get; set; }
    public CustomerViewModel CustomerToCreate { get; set; }
    public List<SelectListItem> Genders { get; set; }
    public List<SelectListItem> Countries { get; set; }



    public void OnGet()
    {
        FillGenderList();
        FillCountriesList();
    }

    private void FillGenderList()
    {
        Genders = Enum.GetValues<Gender>()
            .Select(g => new SelectListItem
            {
                Value = g.ToString(),
                Text = g.ToString()
            }).ToList();
    }

    private void FillCountriesList()
    {
        Countries = Enum.GetValues<Country>()
     .Select(g => new SelectListItem
     {
         Value = g.ToString(),
         Text = g.ToString()
     }).ToList();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            if (CustomerToCreate.Country == "Norway")
            {
                CustomerToCreate.CountryCode = "NO";
            }
            if (CustomerToCreate.Country == "Sweden")
            {
                CustomerToCreate.CountryCode = "SE";
            }
            if (CustomerToCreate.Country == "Finland")
            {
                CustomerToCreate.CountryCode = "FI";
            }
            if (CustomerToCreate.Country == "Denmark")
            {
                CustomerToCreate.CountryCode = "DK";
            }

            _customerService.CreateCustomer(
                CustomerToCreate.Gender,
                CustomerToCreate.Givenname,
                CustomerToCreate.Surname,
                CustomerToCreate.Streetaddress,
                CustomerToCreate.City,
                CustomerToCreate.Zipcode,
                CustomerToCreate.Country,
                CustomerToCreate.CountryCode,
                CustomerToCreate.Birthday,
                CustomerToCreate.NationalId,
                CustomerToCreate.Telephonecountrycode,
                CustomerToCreate.Telephonenumber,
                CustomerToCreate.Emailaddress
                );

            return RedirectToPage("Index");
        }
        FillGenderList();
        FillCountriesList();
        return Page();
    }

}
