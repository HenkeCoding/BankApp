using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly BankAppDataContext _dbContext;
        public CountryService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> GetCustomersByCountryCode(string countryCode)
        {
            return _dbContext.Customers
                .Where(c => c.CountryCode == countryCode)
                .ToList();
        }

        public IEnumerable<Account> GetAccountsByCountryCode(string countryCode)
        {
            return _dbContext.Dispositions
                .Where(a => a.Customer.CountryCode == countryCode)
                .Select(a => a.Account)
                .ToList();
        }

        public decimal GetBalanceSumByCountryCode(string countryCode)
        {
            return _dbContext.Dispositions
                .Where(a => a.Customer.CountryCode == countryCode)
                .Sum(a => a.Account.Balance);
        }
    }
}
