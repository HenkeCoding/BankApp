using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;
using System.ComponentModel.DataAnnotations;

namespace BankTemplateInclDataAccessLayer.Pages.Person
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public EditModel(ICustomerService customerService)
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


        public void OnGet(int customerId)
        {
            var customerDb = _customerService.GetCustomer(customerId);
            Gender = customerDb.Gender;
            Givenname = customerDb.Givenname;
            Surname = customerDb.Surname;
            Streetaddress = customerDb.Streetaddress;

            City = customerDb.City;
            Zipcode = customerDb.Zipcode;
            Country = customerDb.Country;

            CountryCode = customerDb.CountryCode;
            Birthday = customerDb.Birthday;
            NationalId = customerDb.NationalId;

            Telephonecountrycode = customerDb.Telephonecountrycode;
            Telephonenumber = customerDb.Telephonenumber;
            Emailaddress = customerDb.Emailaddress;
        }

        public IActionResult OnPost(int customerId)
        {
            if (ModelState.IsValid)
            {
                var customerDb = _customerService.GetCustomer(customerId);
                customerDb.Givenname = Givenname;
                customerDb.Surname = Surname;
                customerDb.Streetaddress = Streetaddress;

                customerDb.City = City;
                customerDb.Zipcode = Zipcode;
                customerDb.Country = Country;

                customerDb.CountryCode = CountryCode;
                customerDb.Birthday = Birthday;
                customerDb.NationalId = NationalId;

                customerDb.Telephonecountrycode = Telephonecountrycode;
                customerDb.Telephonenumber = Telephonenumber;
                customerDb.Emailaddress = Emailaddress;

                _customerService.Update(customerDb);
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
