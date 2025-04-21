namespace BankTemplateInclDataAccessLayer.ViewModels
{
    public class CountryBoxViewModel
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public ICollection<CustomerViewModel> Customers { get; set; } = new List<CustomerViewModel>();
        public ICollection<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();
        public decimal BalanceSum { get; set; }
    }
}
