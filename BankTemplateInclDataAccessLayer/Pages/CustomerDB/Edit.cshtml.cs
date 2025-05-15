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
public class EditModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;

    public EditModel(ICustomerService customerService, IMapper mapper, ICountryService countryService)
    {
        _customerService = customerService;
        _mapper = mapper;
        _countryService = countryService;
    }

    public int CustomerId { get; set; }

    [Range(1, 4, ErrorMessage =
"Please choose a valid country!")]
    public int CountryId { get; set; }
    public CustomerViewModel UpdatedCustomerInfo { get; set; } = new CustomerViewModel();

    public List<SelectListItem> Genders { get; set; }

    public List<SelectListItem> Countries { get; set; }

    public void OnGet(int customerId)
    {
        var customerDb = _customerService.GetCustomer(customerId);

        _mapper.Map(customerDb, UpdatedCustomerInfo);

        if (customerDb == null)
        {
            RedirectToPage("Index");
        }

        CountryId = _countryService.GetCountryByCode(customerDb.CountryCode).CountryId;

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
        Countries = new List<SelectListItem>();

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

    public IActionResult OnPost(int customerId)
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

            UpdatedCustomerInfo.CountryCode = country.CountryCode;
            UpdatedCustomerInfo.Country = country.CountryName;


            var customerDb = _customerService.GetCustomer(customerId);


            _mapper.Map(UpdatedCustomerInfo, customerDb);

            _customerService.Update(customerDb);

            return RedirectToPage("Index", TempData["Message"] = $"Customer number {customerDb.CustomerId} was updated.");
        }

        FillGenderList();
        FillCountryList();
        return Page();
    }

}
