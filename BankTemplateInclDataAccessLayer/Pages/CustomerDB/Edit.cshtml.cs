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
public class EditModel : PageModel
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public EditModel(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }

    public int CustomerId { get; set; }
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

    public IActionResult OnPost(int customerId)
    {
        if (ModelState.IsValid)
        {
            var customerDb = _customerService.GetCustomer(customerId);

            if (UpdatedCustomerInfo.Country == "Norway")
            {
                UpdatedCustomerInfo.CountryCode = "NO";
            }
            if (UpdatedCustomerInfo.Country == "Sweden")
            {
                UpdatedCustomerInfo.CountryCode = "SE";
            }
            if (UpdatedCustomerInfo.Country == "Finland")
            {
                UpdatedCustomerInfo.CountryCode = "FI";
            }
            if (UpdatedCustomerInfo.Country == "Denmark")
            {
                UpdatedCustomerInfo.CountryCode = "DK";
            }

            _mapper.Map(UpdatedCustomerInfo, customerDb);

            _customerService.Update(customerDb);
            return RedirectToPage("Index");
        }

        FillGenderList();
        FillCountriesList();
        return Page();
    }

}
