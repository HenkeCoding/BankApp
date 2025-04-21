using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public interface ICountryService
    {
        IEnumerable<Customer> GetCustomersByCountryCode(string countryCode);
        IEnumerable<Account> GetAccountsByCountryCode(string countryCode);
        decimal GetBalanceSumByCountryCode(string countryCode);
    }
}