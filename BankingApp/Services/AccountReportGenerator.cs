using System;
using BankingApp.Interfaces;
using BankingApp.Models;

namespace BankingApp.Services;

public class AccountReportGenerator : IMonthlyReportGenerator, IQuarterlyReportGenerator, IYearlyReportGenerator
{
    private readonly IBankAccount _account;

    public AccountReportGenerator(IBankAccount account)
    {
        _account = account;
    }

    public void GenerateMonthlyReport(Transaction[] transactions, DateOnly reportDate)
    {
        Console.WriteLine($"Generating the {reportDate:MMMM} {reportDate.Year} report for account: {_account.AccountNumber}");

        Console.WriteLine($"Account Number: {_account.AccountNumber}");
        Console.WriteLine($"Account Type: {_account.AccountType}");
        Console.WriteLine($"Account Balance: {_account.Balance}");

        // Generating monthly report based on transaction history
        foreach (var transaction in transactions)
        {
            try
            {
                if (transaction.TransactionDate.Month == reportDate.Month && transaction.TransactionDate.Year == reportDate.Year)
                {
                    Console.WriteLine($"{transaction.ReturnTransaction()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public void GenerateCurrentMonthToDateReport(Transaction[] transactions, DateOnly endDate)
    {
        Console.WriteLine($"Generating current month-to-date report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GeneratePrevious30DayReport(Transaction[] transactions, DateOnly endDate)
    {
        Console.WriteLine($"Generating previous 30 days report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateQuarterlyReport(Transaction[] transactions, DateOnly reportDate)
    {
        Console.WriteLine($"Generating quarterly report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GeneratePreviousYearReport(Transaction[] transactions, DateOnly reportDate)
    {
        Console.WriteLine($"Generating previous year report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateCurrentYearToDateReport(Transaction[] transactions, DateOnly endDate)
    {
        Console.WriteLine($"Generating current year-to-date report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateLast365DaysReport(Transaction[] transactions, DateOnly endDate)
    {
        Console.WriteLine($"Generating last 365 days report for {_account.AccountType} account number: {_account.AccountNumber}");
    }
}
