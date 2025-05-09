using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BankApp.ViewModels;
using Services.Services;

namespace BankApp.Pages.Countries
{
    [ResponseCache(Duration = 60, VaryByQueryKeys = ["countryId"])]

    public class CountryImageModel : PageModel
    {
        private readonly ICountryService _countryService;
        private readonly ICustomerBalanceService _customerBalanceService;
        public CountryImageModel(ICountryService countryService, ICustomerBalanceService customerBalanceService)
        {
            _countryService = countryService;
            _customerBalanceService = customerBalanceService;
        }

        public CountryViewModel Country { get; set; }

        public List<CustomerViewModel> Customers { get; set; }


        public void OnGet(int countryId)
        {
            if (countryId == null || countryId == 0)
            {
                RedirectToPage("Index");
            }

            var CountryDb = _countryService.GetCountryById(countryId);

            Country = new CountryViewModel
            {
                CountryId = CountryDb.CountryId,
                CountryCode = CountryDb.CountryCode,
                CountryName = CountryDb.CountryName
            };


            Customers = _countryService.GetTopCustomersForCountryByCountryCode(Country.CountryCode)
                .Select(x => new CustomerViewModel {

                ViewId = x.CustomerId,
                Givenname = x.Givenname,
                Surname = x.Surname,

                Streetaddress = x.Streetaddress,
                City = x.City,
                NationalId = x.NationalId,


                CountryCode = x.CountryCode,
                TotalBalance = _customerBalanceService.GetCustomerTotalBalanceByCustomerId(x.CustomerId),
                }).ToList();

        }
    }
}
