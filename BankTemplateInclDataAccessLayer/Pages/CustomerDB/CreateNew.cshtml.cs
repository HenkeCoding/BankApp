using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace BankTemplateInclDataAccessLayer.Pages.CustomerDB;

[BindProperties]
[Authorize(Roles = "Cashier, Admin")]
public class CreateNewModel : PageModel
{
    private readonly ICustomerService _customerService;
    public CreateNewModel(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    public int CustomerId { get; set; }

    public string Gender { get; set; } = null!;

    [MaxLength(100)][Required] public string Givenname { get; set; }

    [MaxLength(100)][Required] public string Surname { get; set; }

    [StringLength(100)] public string Streetaddress { get; set; }

    [StringLength(50)][Required] public string City { get; set; }

    [StringLength(10)] public string Zipcode { get; set; }

    public string Country { get; set; }

    [StringLength(2)] public string CountryCode { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? NationalId { get; set; }

    public string? Telephonecountrycode { get; set; }

    public string? Telephonenumber { get; set; }

    [StringLength(150)][EmailAddress] public string? Emailaddress { get; set; }



    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _customerService.CreateCustomer(
                Gender,
                Givenname,
                Surname,
                Streetaddress,
                City,
                Zipcode,
                Country,
                CountryCode,
                Birthday,
                NationalId,
                Telephonecountrycode,
                Telephonenumber,
                Emailaddress
                );

            return RedirectToPage("Index");
        }
        return Page();
    }

}
