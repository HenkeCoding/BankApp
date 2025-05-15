using DataAccessLayer.Models;

namespace Services.Services;

public interface ICreateCustomerService
{
    Customer CreateCustomer(
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
    );
}
