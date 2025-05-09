using Services.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace BankApp.ViewModels
{
    public class CustomerViewModel
    {
        public int ViewId { get; set; }

        [ValidGenderAttribute]
        public string Gender { get; set; }

        [MaxLength(100)][Required] public string Givenname { get; set; }

        [MaxLength(100)][Required] public string Surname { get; set; }

        [StringLength(100)] public string Streetaddress { get; set; }

        [StringLength(50)][Required] public string City { get; set; }

        [StringLength(10)] public string Zipcode { get; set; }

        public string? Country { get; set; }

        [StringLength(2)] public string? CountryCode { get; set; }

        public DateOnly? Birthday { get; set; }

        public string? NationalId { get; set; }

        public string? Telephonecountrycode { get; set; }

        public string? Telephonenumber { get; set; }
        [StringLength(150)][EmailAddress] public string? Emailaddress { get; set; }

        public decimal? TotalBalance { get; set; }

    }
}
