using System;
using BankingApp.Interfaces;


namespace BankingApp.Services;

public class AccountReportGenerator : IMonthlyReportGenerator, IQuarterlyReportGenerator, IYearlyReportGenerator
{
    private readonly IBankAccount _account;

    public AccountReportGenerator(IBankAccount account)
    {
        _account = account;
    }

    public void GenerateMonthlyReport()
    {
        Console.WriteLine($"Generating monthly report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateCurrentMonthToDateReport()
    {
        Console.WriteLine($"Generating current month-to-date report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GeneratePrevious30DayReport()
    {
        Console.WriteLine($"Generating previous month report for for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateQuarterlyReport()
    {
        Console.WriteLine($"Generating quarterly report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GeneratePreviousYearReport()
    {
        Console.WriteLine($"Generating previous year report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateCurrentYearToDateReport()
    {
        Console.WriteLine($"Generating current year-to-date report for {_account.AccountType} account number: {_account.AccountNumber}");
    }

    public void GenerateLast365DaysReport()
    {
        Console.WriteLine($"Generating last 365 days report for {_account.AccountType} account number: {_account.AccountNumber}");
    }
}
