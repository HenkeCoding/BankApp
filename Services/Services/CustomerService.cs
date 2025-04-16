using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;
public class CustomerService : ICustomerService
{
    private readonly BankAppDataContext _dbContext;

    public CustomerService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public PagedResult<Customer> GetCustomers(
        int pageNo,
        int pageSize,
        string sortColumn,
        string sortOrder,
        string q
        )
    {
        IEnumerable<Customer> query = _dbContext.Customers;

        if (!string.IsNullOrEmpty(q))
        {
            query = query
                .Where(p => p.Givenname.Contains(q) ||
                p.Surname.Contains(q));
        }


        return query.GetPaged(pageNo, pageSize);
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

