namespace BankTemplateInclDataAccessLayer.ViewModels
{
    public class DispositionViewModel
    {
        public int DispositionId { get; set; }

        public int CustomerId { get; set; }

        public int AccountId { get; set; }

        public string Type { get; set; } = null!;

        public AccountViewModel Account { get; set; } = null!;
    }
}
