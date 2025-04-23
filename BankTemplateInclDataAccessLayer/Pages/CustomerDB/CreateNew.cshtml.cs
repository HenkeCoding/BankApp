using AutoMapper;
using BankApp.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Services;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Pages.CustomerDB;

[BindProperties]
[Authorize(Roles = "Cashier, Admin")]
public class CreateNewModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    public CreateNewModel(ICustomerService customerService, IMapper mapper, ICountryService countryService)
    {
        _customerService = customerService;
        _mapper = mapper;
        _countryService = countryService;
    }


    public int CustomerId { get; set; }
    [Range(1, 4, ErrorMessage =
"Please choose a valid country!")]
    public int CountryId { get; set; }
    public CustomerViewModel CustomerToCreate { get; set; }
    public List<SelectListItem> Genders { get; set; }
    public List<SelectListItem> Countries { get; set; }



    public void OnGet()
    {
        FillGenderList();
        FillCountryList();
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

    private void FillCountryList()
    {
        Countries.Add(new SelectListItem
        {
            Text = "Choose",
            Value = "0"
        });

        var countriesList = _countryService.GetCountries()
        .Select(c => new SelectListItem
        {
            Text = c.CountryName,
            Value = c.CountryId.ToString()
        }).ToList();

        foreach (var country in countriesList)
        {
            Countries.Add(country);
        }
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            var country = _countryService.GetCountryById(CountryId);

            if (country == null)
            {
                ModelState.AddModelError("CountryId", "Please choose a valid country!");
                FillGenderList();
                FillCountryList();
                return Page();
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
        FillCountryList();
        return Page();
    }

}
