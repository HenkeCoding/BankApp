using DataAccessLayer.Models;

namespace Services.Services;
public class CustomerService : ICustomerService
{
    private readonly BankAppDataContext _dbContext;

    public CustomerService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Customer> GetCustomers()
    {
        return _dbContext.Customers.ToList();
    }

    public Customer GetCustomer(int customerId)
    {
        return _dbContext.Customers.First(a => a.CustomerId == customerId);
    }

    public void Update(Customer customer)
    {
        _dbContext.SaveChanges();
    }
}

