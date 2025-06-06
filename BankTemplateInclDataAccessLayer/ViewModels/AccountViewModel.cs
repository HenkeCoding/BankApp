﻿namespace BankApp.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }
    }
}
