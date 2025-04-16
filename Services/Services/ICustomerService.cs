using DataAccessLayer.Models;
using Services.Infrastructure.Paging;

namespace Services.Services;

public interface ICustomerService
{
    PagedResult<Customer> GetCustomers(
        int pageNo,
        int pageSize,
        string sortColumn,
        string sortOrder,
        string q
        );
    void Update(Customer customer);
    Customer GetCustomer(int customerId);
}
