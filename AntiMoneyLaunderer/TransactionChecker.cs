using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace AntiMoneyLaunderer;

public class TransactionChecker
{
    public void IsTransactionSuspicious(string CountryCode, BankAppDataContext bankAppDataContext)
    {
        string countryCode = CountryCode;
        var _dbContext = bankAppDataContext;

        // Fix: Use a lambda expression to access the CountryCode property of each Customer object  
        var customers = _dbContext.Customers.Where(customer => customer.CountryCode == countryCode);

        var dispositions = _dbContext.Dispositions.Where(disposition => customers.Any(customer => customer.CustomerId == disposition.CustomerId)).ToList();

        var dispositionAccountIds = dispositions.Select(d => d.AccountId).ToList();
        var accounts = _dbContext.Accounts.Where(account => dispositionAccountIds.Contains(account.AccountId)).ToList();
        var accountIds = accounts.Select(a => a.AccountId).ToList();
        var transactions = _dbContext.Transactions.Where(transaction => accountIds.Contains(transaction.AccountId)).ToList();


        // Get the date 3 days ago
        DateTime threeDaysAgo = DateTime.Now.AddDays(-3);

        // Filter transactions from the last 3 days
        var recentTransactions = transactions
            .Where(t => t.Date.ToDateTime(TimeOnly.MinValue) >= threeDaysAgo)
            .ToList();

        // Group by AccountId and sum amounts
        var qualifyingAccountIds = recentTransactions
            .GroupBy(t => t.AccountId)
            .Where(g => g.Sum(t => t.Amount) >= 21000)
            .Select(g => g.Key)
            .ToList();

        // Collect all transactions from the last 3 days for those accounts
        var suspiciousTransactions = recentTransactions
            .Where(t => qualifyingAccountIds.Contains(t.AccountId))
            .ToList();


        foreach (Transaction transaction in transactions)
        {
            if (suspiciousTransactions.Contains(transaction))
            {
                continue;
            }

            if (transaction.Amount > 15000)
            {
                suspiciousTransactions.Add(transaction);
            }
        }

        // Read through the old textfiles and remove the transactions that are in them from the suspicious transactions list

        var oldFiles = Directory.GetFiles("../../../textfiles", $"suspicioustransactions{countryCode}*.txt");
        foreach (var file in oldFiles)
        {
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length > 0)
                {
                    var transactionId = parts[0].Split(':')[1].Trim();
                    var transaction = suspiciousTransactions.FirstOrDefault(t => t.TransactionId.ToString() == transactionId);
                    if (transaction != null)
                    {
                        suspiciousTransactions.Remove(transaction);
                    }
                }
            }
        }

        List<string> suspiciousTransactionsList = new List<string>();

        foreach (var transaction in suspiciousTransactions)
        {
            suspiciousTransactionsList.Add($"Transaction ID: {transaction.TransactionId}, Account ID: {transaction.AccountId}, Amount: {transaction.Amount}, Type: {transaction.Type}, Operation: {transaction.Operation}, Date: {transaction.Date} \n");
        }

        string suspiciousTransactionsString = string.Join("", suspiciousTransactionsList.ToArray());

        File.AppendAllText($"../../../textfiles/suspicioustransactions{countryCode}{DateTime.Now:yyyy-MM-dd}.txt", suspiciousTransactionsString);
    }
}
