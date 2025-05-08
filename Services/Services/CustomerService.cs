using AutoMapper;
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
        string q,
        string searchSubject
        )
    {
        IEnumerable<Customer> query = _dbContext.Customers;

        if (q != null)
        {
            q = q.ToLower();
        }


        if (!string.IsNullOrEmpty(q))
        {
            if (searchSubject == "Name")
            {
                query = query
                    .Where(p => p.Givenname.ToLower().Contains(q) ||
                    p.Surname.ToLower().Contains(q));
            }
            else if (searchSubject == "City")
            {
                query = query
                    .Where(p => p.City.ToLower().Contains(q));
            }



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

        if (sortColumn == "City")
            if (sortOrder == "asc")
                query = query.OrderBy(s => s.City);
            else if (sortOrder == "desc")
                query = query.OrderByDescending(s => s.City);



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

