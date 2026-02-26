using System;
using System.Globalization;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    static void Main()
    {
        string firstName = "Tim";
        string lastName = "Shao";
        BankCustomer customer1 = new StandardCustomer(firstName, lastName);

        BankAccount account1 = new BankAccount(customer1.CustomerId, 10000);
        BankAccount account2 = new CheckingAccount(customer1.CustomerId, 500, 400);
        BankAccount account3 = new SavingsAccount(customer1.CustomerId, 1000);

        BankAccount[] bankAccounts = new BankAccount[4];
        bankAccounts[0] = account1;
        bankAccounts[1] = account2;
        bankAccounts[2] = account3;

        Transaction[] datedTransactions = new Transaction[5];

        Console.WriteLine("\nDemonstrate date and time operations:");

        DateTime currentDateTime = DateTime.Now;
        Console.WriteLine($"Current date and time: {currentDateTime}");

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        Console.WriteLine($"Current date: {currentDate}");

        TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
        Console.WriteLine($"Current time: {currentTime}");

        DayOfWeek currentDayOfWeek = DateTime.Now.DayOfWeek;
        Console.WriteLine($"Current day of the week: {currentDayOfWeek}");

        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;
        Console.WriteLine($"Current month: {currentMonth}, Current year: {currentYear}");

        DateTime datePlusDays = DateTime.Now.AddDays(10);
        Console.WriteLine($"Date plus 10 days: {datePlusDays}");

        DateTime parsedDate = DateTime.Parse("2026-02-26");
        Console.WriteLine($"Parsed date: {parsedDate}");

        string formattedDate = DateTime.Now.ToString("yyyy-MM-dd");
        Console.WriteLine($"Formatted date: {formattedDate}");

        TimeZoneInfo currentTimeZone = TimeZoneInfo.Local;
        TimeSpan offsetFromUtc = currentTimeZone.GetUtcOffset(DateTime.Now);
        Console.WriteLine($"Current time zone: {currentTimeZone.DisplayName}, offset from UTC: {offsetFromUtc}");

        DateTime utcTime = DateTime.UtcNow;
        Console.WriteLine($"UTC Time: {utcTime}");

        datedTransactions[0] = new Transaction(currentDate, currentTime, 100, account2.AccountNumber, account2.AccountNumber, "Withdraw", "Groceries");
        datedTransactions[1] = new Transaction(currentDate.AddDays(-1), new TimeOnly(13, 15), 500, account2.AccountNumber, account2.AccountNumber, "Deposit", "ATM Deposit");
        datedTransactions[2] = new Transaction(new DateOnly(2025, 12, 1), new TimeOnly(10, 0), 200, account2.AccountNumber, account2.AccountNumber, "Deposit", "Salary");
        datedTransactions[3] = new Transaction(new DateOnly(2025, 12, 2), new TimeOnly(14, 30), 150, account2.AccountNumber, account2.AccountNumber, "Withdraw", "Groceries");
        datedTransactions[4] = new Transaction(new DateOnly(2025, 12, 3), new TimeOnly(9, 45), 300, account2.AccountNumber, account2.AccountNumber, "Deposit", "Freelance Work");

        Console.WriteLine("\nDated transactions:");
        foreach (var transaction in datedTransactions)
        {
            Console.WriteLine(transaction.ReturnTransaction());
        }

        DateOnly startDate = new(2025, 10, 12);
        DateOnly endDate = new(2025, 12, 20);

        List<Transaction> transactions = new(SimulateTransactions.SimulateTransactionsDateRange(startDate, endDate, account2, account3));

        Console.WriteLine("\nSimulated Transactions:");
        foreach (var transaction in transactions)
        {
            if (transaction != null)
            {
                Console.WriteLine(transaction.ReturnTransaction());
            }
        }

        Console.WriteLine($"\nNumber of transactions processed: {transactions.Count}");
    }
}