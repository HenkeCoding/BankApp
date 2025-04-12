using DataAccessLayer.Models;

namespace Services.Services;

public interface ICustomerService
{
    List<Customer> GetCustomers();
    void Update(Customer customer);
    Customer GetCustomer(int customerId);
}
