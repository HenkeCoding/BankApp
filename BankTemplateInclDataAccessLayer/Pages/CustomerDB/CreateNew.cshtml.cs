using AutoMapper;
using BankApp.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Services.Infrastructure.Validation;
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
    private readonly ICreateCustomerService _createCustomerService;
    public CreateNewModel(
        ICustomerService customerService,
        IMapper mapper,
        ICountryService countryService,
        ICreateCustomerService createCustomerService
        )
    {
        _customerService = customerService;
        _mapper = mapper;
        _countryService = countryService;
        _createCustomerService = createCustomerService;
    }


    public int CustomerId { get; set; }


    [Range(1, 4, ErrorMessage =
"Please choose a valid country!")]
    public int CountryId { get; set; }


    public CustomerViewModel CustomerToCreate { get; set; }
    public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> AccountFrequencies { get; set; } = new List<SelectListItem>();


    [ValidFrequencyAttribute]
    public string AccountFrequency { get; set; }


    public void OnGet()
    {
        FillGenderList();
        FillCountryList();
        FillFrequencyList();
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

    private void FillFrequencyList()
    {
        AccountFrequencies = Enum.GetValues<AccountFrequency>()
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
            var country = _countryService.GetCountryById(CountryId);

            if (country == null)
            {
                ModelState.AddModelError("CountryId", "Please choose a valid country!");
                FillGenderList();
                FillCountryList();
                FillFrequencyList();
                return Page();
            }

            var customerJustCreated = _createCustomerService.CreateCustomer(
                CustomerToCreate.Gender,
                CustomerToCreate.Givenname,
                CustomerToCreate.Surname,
                CustomerToCreate.Streetaddress,
                CustomerToCreate.City,
                CustomerToCreate.Zipcode,

                country.CountryName,
                country.CountryCode,

                CustomerToCreate.Birthday,
                CustomerToCreate.NationalId,
                CustomerToCreate.Telephonecountrycode,
                CustomerToCreate.Telephonenumber,
                CustomerToCreate.Emailaddress,

                AccountFrequency
            );

            return RedirectToPage("Index", TempData["Message"] = $"A new customer was successfully created with the ID: {customerJustCreated.CustomerId}");
        }

        FillGenderList();
        FillCountryList();
        FillFrequencyList();
        return Page();
    }
}
