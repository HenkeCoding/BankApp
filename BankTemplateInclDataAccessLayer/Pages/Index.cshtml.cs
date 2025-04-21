using BankTemplateInclDataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace BankTemplateInclDataAccessLayer.Pages;

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

        Countries.Add(new CountryBoxViewModel
        {
            CountryCode = "NO",
            CountryName = "Norway",
            Customers = _countryService.GetCustomersByCountryCode("NO").Select(c => new CustomerViewModel { }).ToList(),
            Accounts = _countryService.GetAccountsByCountryCode("NO").Select(a => new AccountViewModel { }).ToList(),
            BalanceSum = _countryService.GetBalanceSumByCountryCode("NO")
        });

        Countries.Add(new CountryBoxViewModel
        {
            CountryCode = "SE",
            CountryName = "Sweden",
            Customers = _countryService.GetCustomersByCountryCode("SE").Select(c => new CustomerViewModel { }).ToList(),
            Accounts = _countryService.GetAccountsByCountryCode("SE").Select(a => new AccountViewModel { }).ToList(),
            BalanceSum = _countryService.GetBalanceSumByCountryCode("SE")
        });

        Countries.Add(new CountryBoxViewModel { 
            CountryCode = "FI", 
            CountryName = "Finland",
            Customers = _countryService.GetCustomersByCountryCode("FI").Select(c => new CustomerViewModel { }).ToList(),
            Accounts = _countryService.GetAccountsByCountryCode("FI").Select(a => new AccountViewModel { }).ToList(),
            BalanceSum = _countryService.GetBalanceSumByCountryCode("FI")
        });

        Countries.Add(new CountryBoxViewModel { 
            CountryCode = "DK", 
            CountryName = "Denmark",
            Customers = _countryService.GetCustomersByCountryCode("DK").Select(c => new CustomerViewModel { }).ToList(),
            Accounts = _countryService.GetAccountsByCountryCode("DK").Select(a => new AccountViewModel { }).ToList(),
            BalanceSum = _countryService.GetBalanceSumByCountryCode("DK")
        });



    }
}
