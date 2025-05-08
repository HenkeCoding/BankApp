using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace Services.Services
{
    public class CustomerBalanceService : ICustomerBalanceService
    {
        private readonly BankAppDataContext _dbContext;
        public CustomerBalanceService(BankAppDataContext context) 
        {
            _dbContext = context;
        }

        public decimal GetCustomerTotalBalanceByCustomerId(int customerId)
        {
            var customer = _dbContext.Customers
                .FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            var balance = _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Sum(d => d.Account.Balance);
            return balance;
        }
    }
}
