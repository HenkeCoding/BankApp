namespace DataAccessLayer.Models;

public partial class Country
{
    public int CountryId { get; set; }
    public string CountryName { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
