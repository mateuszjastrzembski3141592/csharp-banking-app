using System;
using BankingApp.Interfaces;
using BankingApp.Models;

namespace BankingApp.Services;

public class CustomerReportGenerator // : IMonthlyReportGenerator, IQuarterlyReportGenerator, IYearlyReportGenerator
{
    private readonly IBankCustomer _customer;

    public CustomerReportGenerator(IBankCustomer customer)
    {
        _customer = customer;
    }

    public void GenerateMonthlyReport(BankAccount account1, Transaction[] transactions1, DateOnly reportDate, BankAccount account2)
    {
        Console.WriteLine($"\nGenerating the {reportDate:MMMM} {reportDate.Year} report for customer: {_customer.ReturnFullName()}");

        // Display customer information
        Console.WriteLine($"Customer Name: {_customer.ReturnFullName()}");
        Console.WriteLine($"Customer ID: {_customer.CustomerId}");

        // Display the properties of the account object
        Console.WriteLine($"Information for account number: {account1.AccountNumber}");
        Console.WriteLine($"Account Type: {account1.AccountType}");
        Console.WriteLine($"Account Balance: {account1.Balance}");

        // Calculate the total deposits, withdrawals, fees, and refunds for accounts
        double account1TotalDeposits = 0;
        double account1TotalWithdrawals = 0;
        double account1TotalFees = 0;
        double account1TotalRefunds = 0;
        int account1TotalTransactions = 0;

        double account2TotalDeposits = 0;
        double account2TotalWithdrawals = 0;
        double account2TotalFees = 0;
        double account2TotalRefunds = 0;
        int account2TotalTransactions = 0;

        int customerTransactionCount = 0;

        foreach (var transaction in transactions1)
        {
            try
            {
                if (transaction.TransactionDate.Month == reportDate.Month && transaction.TransactionDate.Year == reportDate.Year)
                {
                    if (transaction.SourceAccountNumber == transaction.TargetAccountNumber && transaction.SourceAccountNumber == account1.AccountNumber)
                    {
                        account1TotalTransactions++;
                        customerTransactionCount++;
                        switch (transaction.TransactionType)
                        {
                            case "Deposit":
                                account1TotalDeposits += transaction.TransactionAmount;
                                break;
                            case "Withdraw":
                                account1TotalWithdrawals += transaction.TransactionAmount;
                                break;
                            case "Bank Fee":
                                account1TotalFees += transaction.TransactionAmount;
                                break;
                            case "Bank Refund":
                                account1TotalRefunds += transaction.TransactionAmount;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (transaction.SourceAccountNumber == transaction.TargetAccountNumber && transaction.SourceAccountNumber == account2.AccountNumber)
                    {
                        account2TotalTransactions++;
                        customerTransactionCount++;
                        switch (transaction.TransactionType)
                        {
                            case "Deposit":
                                account2TotalDeposits += transaction.TransactionAmount;
                                break;
                            case "Withdraw":
                                account2TotalWithdrawals += transaction.TransactionAmount;
                                break;
                            case "Bank Fee":
                                account2TotalFees += transaction.TransactionAmount;
                                break;
                            case "Bank Refund":
                                account2TotalRefunds += transaction.TransactionAmount;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (transaction.SourceAccountNumber == account1.AccountNumber && transaction.TargetAccountNumber == account2.AccountNumber)
                    {
                        account1TotalTransactions++;
                        account2TotalTransactions++;
                        customerTransactionCount++;
                        switch (transaction.TransactionType)
                        {
                            case "Transfer":
                                account1TotalWithdrawals += transaction.TransactionAmount;
                                account2TotalDeposits += transaction.TransactionAmount;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (transaction.SourceAccountNumber == account2.AccountNumber && transaction.TargetAccountNumber == account1.AccountNumber)
                    {
                        account1TotalTransactions++;
                        account2TotalTransactions++;
                        customerTransactionCount++;
                        switch (transaction.TransactionType)
                        {
                            case "Transfer":
                                account2TotalWithdrawals += transaction.TransactionAmount;
                                account1TotalDeposits += transaction.TransactionAmount;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Report the totals
        Console.WriteLine($"\nActivity for {account1.AccountType} account number: {account1.AccountNumber}");
        Console.WriteLine($"Total Deposits: {account1TotalDeposits:C}");
        Console.WriteLine($"Total Withdrawals: {account1TotalWithdrawals:C}");
        Console.WriteLine($"Total Fees: {account1TotalFees:C}");
        Console.WriteLine($"Total Refunds: {account1TotalRefunds:C}");

        Console.WriteLine($"\nActivity for {account2.AccountType} account number: {account2.AccountNumber}");
        Console.WriteLine($"Total Deposits: {account2TotalDeposits:C}");
        Console.WriteLine($"Total Withdrawals: {account2TotalWithdrawals:C}");
        Console.WriteLine($"Total Fees: {account2TotalFees:C}");
        Console.WriteLine($"Total Refunds: {account2TotalRefunds:C}");

        Console.WriteLine($"\nTotal Transactions for {account1.AccountType} account number {account1.AccountNumber}: {account1TotalTransactions}");
        Console.WriteLine($"Total Transactions for {account2.AccountType} account number {account2.AccountNumber}: {account2TotalTransactions}");

        Console.WriteLine($"\nTotal Transactions for customer {_customer.ReturnFullName()}: {customerTransactionCount}");
    }

    public void GenerateCurrentMonthToDateReport()
    {
        Console.WriteLine($"Generating current month-to-date report for customer: {_customer.ReturnFullName()}");
    }

    public void GeneratePrevious30DayReport()
    {
        Console.WriteLine($"Generating previous month report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateQuarterlyReport()
    {
        Console.WriteLine($"Generating quarterly report for customer: {_customer.ReturnFullName()}");
    }

    public void GeneratePreviousYearReport()
    {
        Console.WriteLine($"Generating previous year report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateCurrentYearToDateReport()
    {
        Console.WriteLine($"Generating current year-to-date report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateLast365DaysReport()
    {
        Console.WriteLine($"Generating last 365 days report for customer: {_customer.ReturnFullName()}");
    }
}
