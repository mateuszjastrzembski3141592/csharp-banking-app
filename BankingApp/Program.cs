using System;
using System.Globalization;
using System.Collections.Generic;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    static void Main()
    {
        Console.WriteLine("Bank Application - demonstrate the use of Collections, HashSets, and Dictionaries.");

        Bank myBank = new();
        Console.WriteLine($"\nBank object created...");

        Console.WriteLine($"\nUse myBank object to add a customer and an account...");
        myBank.AddCustomer(new StandardCustomer("Remy", "Morris"));
        myBank.Customers[0].AddAccount(new BankAccount(myBank.Customers[0], myBank.Customers[0].CustomerId, 1500.00, "Checking"));
        Console.WriteLine($"{myBank.Customers[0].Accounts[0].AccountType} account object created and added to {myBank.Customers[0].ReturnFullName()}'s account collection.");

        Console.WriteLine($"\nAdd new customer and account objects to the bank...");
        StandardCustomer customer1 = new("Ni", "Kang");
        BankAccount account1 = new(customer1, customer1.CustomerId, 1000.00, "Checking");

        customer1.AddAccount(account1);
        myBank.AddCustomer(customer1);

        foreach (BankCustomer bankCustomer in myBank.Customers)
        {
            Console.WriteLine($"{bankCustomer.ReturnFullName()} has {bankCustomer.Accounts.Count} accounts.");
        }

        Console.WriteLine($"\nUse the Customers collection to add SavingsAccount and MoneyMarketAccount to all customers...");

        foreach (BankCustomer bankCustomer in myBank.Customers)
        {
            bankCustomer.AddAccount(new SavingsAccount(bankCustomer, bankCustomer.CustomerId, 3000.00, 6));
            bankCustomer.AddAccount(new MoneyMarketAccount(bankCustomer, bankCustomer.CustomerId, 15000.00, 1000.00));
            Console.WriteLine($"{bankCustomer.ReturnFullName()} has {bankCustomer.Accounts.Count} accounts.");
        }

        Console.WriteLine($"\nGenerate two months of transactions for customer \"Ni Kang\"...");

        foreach (BankCustomer bankCustomer in myBank.Customers)
        {
            if (bankCustomer.ReturnFullName() == "Ni Kang")
            {
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

                DateOnly startDate = currentDate.AddMonths(-2);
                DateOnly endDate = currentDate;
                BankCustomer customer = bankCustomer;

                customer = SimulateDepositsWithdrawalsTransfers.SimulateActivityDateRange(startDate, endDate, customer);

                int totalTransactions = 0;
                foreach (BankAccount account in bankCustomer.Accounts.Cast<BankAccount>())
                {
                    totalTransactions += account.Transactions.Count;
                }
                Console.WriteLine($"{bankCustomer.ReturnFullName()} had {totalTransactions} transactions in the past two months.");
            }
        }

        Console.WriteLine($"\nDisplay all transactions for all accounts...");

        foreach (BankCustomer bankCustomer in myBank.Customers)
        {
            Console.WriteLine($"\nTransactions for {bankCustomer.ReturnFullName()}:");

            foreach (BankAccount account in bankCustomer.Accounts.Cast<BankAccount>())
            {
                Console.WriteLine($"\nAccount Type: {account.AccountType}, Account Number: {account.AccountNumber}");
                foreach (Transaction transaction in account.Transactions)
                {
                    Console.WriteLine(transaction.ReturnTransaction());
                }
            }
        }

        Console.WriteLine("\nMonthly statement showing Transfers, Deposits, and Withdrawals...");

        BankCustomer? reportCustomer = null;

        // Locating a customer
        foreach (BankCustomer bankCustomer in myBank.Customers)
        {
            if (bankCustomer.ReturnFullName() == "Ni Kang")
            {
                reportCustomer = bankCustomer;
                break;
            }
        }

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly reportStart = new DateOnly(today.Year, today.Month, 1).AddMonths(-1);
        DateOnly reportEnd = new(reportStart.Year, reportStart.Month, DateTime.DaysInMonth(reportStart.Year, reportStart.Month));

        // HashSet for tracking unique transfer signatures across accounts
        HashSet<string> uniqueTransferKeys = [];

        // Dictionary for organizing monthly activity by transaction type
        Dictionary<string, List<string>> monthlyActivity = new()
        {
            { "Deposits", new List<string>() },
            { "Withdrawals", new List<string>() },
            { "Transfers", new List<string>() }
        };

        if (reportCustomer != null)
        {
            foreach (BankAccount account in reportCustomer.Accounts.Cast<BankAccount>())
            {
                foreach (Transaction transaction in account.Transactions)
                {
                    if (transaction.TransactionDate < reportStart || transaction.TransactionDate > reportEnd)
                    {
                        continue;
                    }

                    string date = transaction.TransactionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string time = transaction.TransactionTime.ToString("HH:mm", CultureInfo.InvariantCulture);
                    string description = transaction.Description.Replace("-(TRANSFER)", string.Empty).Trim();

                    if (transaction.TransactionType == "Transfer")
                    {
                        // Build a signature that uniquely identifies a single transfer across accounts
                        string signature = $"{date}|{time}|{transaction.TransactionAmount:F2}|{description}";

                        if (uniqueTransferKeys.Add(signature))
                        {
                            monthlyActivity["Transfers"].Add($"{transaction.TransactionDate} {time} - Transfer {transaction.TransactionAmount:C} - {description}");
                        }
                    }
                    else if (transaction.TransactionType == "Deposit")
                    {
                        monthlyActivity["Deposits"].Add($"{transaction.TransactionDate} {time} - Deposit {transaction.TransactionAmount:C} ({account.AccountType})");
                    }
                    else if (transaction.TransactionType == "Withdraw")
                    {
                        monthlyActivity["Withdrawals"].Add($"{transaction.TransactionDate} {time} - Withdrawal {transaction.TransactionAmount:C} ({account.AccountType})");
                    }
                }
            }

            Console.WriteLine($"\nMonthly Statement for {reportCustomer.ReturnFullName()} - {reportStart.ToString("MMMM yyyy", CultureInfo.InvariantCulture)}");
            Console.WriteLine($"Date Range: {reportStart} to {reportEnd}");
            Console.WriteLine($"Summary: Deposits={monthlyActivity["Deposits"].Count}, Withdrawals={monthlyActivity["Withdrawals"].Count}, Transfers (unique)={monthlyActivity["Transfers"].Count}");

            Console.WriteLine("\nTransfers (unique):");
            foreach (var line in monthlyActivity["Transfers"]) Console.WriteLine(line);

            Console.WriteLine("\nDeposits:");
            foreach (var line in monthlyActivity["Deposits"]) Console.WriteLine(line);

            Console.WriteLine("\nWithdrawals:");
            foreach (var line in monthlyActivity["Withdrawals"]) Console.WriteLine(line);
        }

        else
        {
            Console.WriteLine("Customer for monthly statement not found.");
        }
    }
}