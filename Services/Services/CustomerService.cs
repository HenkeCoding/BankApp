using DataAccessLayer.Models;
using Services.Infrastructure.Paging;
using System.Net.Mail;
using System.Reflection.Emit;

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

        if (sortColumn == "Givenname")
            if (sortOrder == "asc")
                query = query.OrderBy(s => s.Givenname);
            else if (sortOrder == "desc")
                query = query.OrderByDescending(s => s.Givenname);

        if (sortColumn == "Surname")
            if (sortOrder == "asc")
                query = query.OrderBy(s => s.Surname);
            else if (sortOrder == "desc")
                query = query.OrderByDescending(s => s.Surname);

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

    public void CreateCustomer(
        string gender,
        string givenname,
        string surname,
        string streetaddress,
        string city,
        string zipcode,
        string country,
        string countryCode,
        DateOnly? birthday,
        string? nationalId,
        string? telephonecountrycode,
        string? telephonenumber,
        string? emailaddress
        )
    {
        var customer = new Customer
        {
            Gender = gender,
            Givenname = givenname,
            Surname = surname,
            Streetaddress = streetaddress,

            City = city,
            Zipcode = zipcode,
            Country = country,

            CountryCode = countryCode,
            Birthday = birthday,
            NationalId = nationalId,

            Telephonecountrycode = telephonecountrycode,
            Telephonenumber = telephonenumber,
            Emailaddress = emailaddress
        };

        Update(customer);
    }
}

