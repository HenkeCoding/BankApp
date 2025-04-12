using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer.Models;

namespace Services.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        void Update(Customer customer);
        Customer GetCustomer(int customerId);
    }
}
