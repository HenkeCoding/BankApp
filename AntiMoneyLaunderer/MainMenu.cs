using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace AntiMoneyLaunderer
{
    public class MainMenu
    {
        private readonly BankAppDataContext _dbContext;
        public MainMenu(BankAppDataContext bankAppDataContext)
        {
            _dbContext = bankAppDataContext;
        }
        public void Run()
        {
            TransactionChecker transactionChecker = new TransactionChecker();


            List<string> countryCodesList = new List<string>();
            
            foreach (var country in _dbContext.Countries)
            {
                countryCodesList.Add(country.CountryCode);
            }

            countryCodesList.Add("Exit");

            string[] countryCodesArray = countryCodesList.ToArray();


            var functionChoiceInput = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(countryCodesArray));


            if (functionChoiceInput == "Exit")
            {
                Console.WriteLine("Exiting...");
                return;
            }
            else
            {
                Console.WriteLine($"You selected: {functionChoiceInput}");

                Console.WriteLine($"Checking for suspicious transactions for country: {functionChoiceInput}...");

                transactionChecker.IsTransactionSuspicious(functionChoiceInput, _dbContext);

                Console.WriteLine("Transaction check complete.");
            }


        }
    }
}
