namespace BankTemplateInclDataAccessLayer.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? Symbol { get; set; }
        public string? Bank { get; set; }
    }
}
