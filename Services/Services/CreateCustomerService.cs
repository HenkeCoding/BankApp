using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Services;

public class CreateCustomerService : ICreateCustomerService
{
    private readonly BankAppDataContext _dbContext;

    public CreateCustomerService(BankAppDataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Customer CreateCustomer(
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
    string? emailaddress,

    string frequency
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

        _dbContext.Customers.Add(customer);

        _dbContext.SaveChanges();


        var account = new Account
        {
            Frequency = frequency,
            Created = DateOnly.FromDateTime(DateTime.Now),
            Balance = 0
        };


        _dbContext.Accounts.Add(account);

        _dbContext.SaveChanges();


        var disposition = new Disposition
        {
            CustomerId = customer.CustomerId,
            AccountId = account.AccountId,
            Type = "OWNER"
        };

        _dbContext.Dispositions.Add(disposition);

        _dbContext.SaveChanges();

        return customer;
    }
}
