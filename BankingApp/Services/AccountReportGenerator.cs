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

    public void GenerateMonthlyReport(BankCustomer bankCustomer, int accountNumber, DateOnly reportDate)
    {

        foreach (BankAccount account in bankCustomer.Accounts.Cast<BankAccount>())
        {
            if (account.AccountNumber == accountNumber)
            {
                Console.WriteLine($"\nGenerating the {reportDate:MMMM} {reportDate.Year} report for {account.AccountType} account: {account.AccountNumber}");

                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Account Type: {account.AccountType}");

                foreach (var transaction in account.Transactions)
                {
                    if (transaction.TransactionDate.Month == reportDate.Month && transaction.TransactionDate.Year == reportDate.Year)
                    {
                        if (transaction.TransactionDate.Day == 1)
                        {
                            double getBalance = transaction.PriorBalance;
                            Console.WriteLine($"Starting Balance on {transaction.TransactionDate}: {getBalance:C}\n");
                        }
                    }
                }

                foreach (var transaction in account.Transactions)
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
                        string errorMessage = ex.Message;
                    }
                }
            }
        }
    }

    public void GenerateCurrentMonthToDateReport(BankCustomer bankCustomer, int accountNumber, DateOnly endDate)
    {
        Console.WriteLine($"\nGenerating current month-to-date report for account: {_account.AccountNumber}");
    }

    public void GeneratePrevious30DayReport(BankCustomer bankCustomer, int accountNumber, DateOnly endDate)
    {
        Console.WriteLine($"Generating previous 30 days report for account: {_account.AccountNumber}");
    }

    public void GenerateQuarterlyReport(BankCustomer bankCustomer, int accountNumber, DateOnly reportDate)
    {
        Console.WriteLine($"Generating quarterly report for account: {_account.AccountNumber}");
    }

    public void GeneratePreviousYearReport(BankCustomer bankCustomer, int accountNumber, DateOnly reportDate)
    {
        Console.WriteLine($"Generating previous year report for account: {_account.AccountNumber}");
    }

    public void GenerateCurrentYearToDateReport(BankCustomer bankCustomer, int accountNumber, DateOnly endDate)
    {
        Console.WriteLine($"Generating current year-to-date report for account: {_account.AccountNumber}");
    }

    public void GenerateLast365DaysReport(BankCustomer bankCustomer, int accountNumber, DateOnly endDate)
    {
        Console.WriteLine($"Generating last 365 days report for account: {_account.AccountNumber}");
    }
}
