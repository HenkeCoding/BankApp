using BankApp.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ICountryService _countryService;

    public IndexModel(ILogger<IndexModel> logger,
        ICountryService countryService)
    {
        _logger = logger;
        _countryService = countryService;

    }

    public List<CountryBoxViewModel> Countries;

    public void OnGet()
    {
        Countries = new List<CountryBoxViewModel>();


        Countries = _countryService.GetCountries()
            .Select(c => new CountryBoxViewModel
            {
                CountryId = c.CountryId,
                CountryCode = c.CountryCode,
                CountryName = c.CountryName,
                Customers = _countryService.GetCustomersByCountryCode(c.CountryCode).Select(c => new CustomerViewModel { }).ToList(),
                Accounts = _countryService.GetAccountsByCountryCode(c.CountryCode).Select(a => new AccountViewModel { }).ToList(),
                BalanceSum = _countryService.GetBalanceSumByCountryCode(c.CountryCode)
            }).ToList();


    }
}
