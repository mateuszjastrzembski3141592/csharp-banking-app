using System;
using BankingApp.Interfaces;
using BankingApp.Services;

namespace BankingApp.Models;

public class BankAccount : IBankAccount
{
    private static int s_nextAccountNumber;
    protected double priorBalance;

    // Task 4: Step 3 - define a private readonly list to store transactions

    public static double TransactionRate { get; private set; }
    public static double MaxTransactionFee { get; private set; }
    public static double OverdraftRate { get; private set; }
    public static double MaxOverdraftFee { get; private set; }

    public int AccountNumber { get; }
    public string CustomerId { get; }
    public double Balance { get; internal set; } = 0;
    public string AccountType { get; set; } = "Checking";
    public BankCustomer Owner { get; }

    public virtual double InterestRate { get; protected set; }

    // Task 4: Step 4 - Add a readonly Transactions property

    static BankAccount()
    {
        Random random = new();
        s_nextAccountNumber = random.Next(10000000, 20000000);
        TransactionRate = 0.01;
        MaxTransactionFee = 10;
        OverdraftRate = 0.05;
        MaxOverdraftFee = 10;
    }

    public BankAccount(BankCustomer owner, string customerIdNumber, double balance = 200, string accountType = "Checking")
    {
        Owner = owner;
        AccountNumber = s_nextAccountNumber++;
        CustomerId = customerIdNumber;
        Balance = balance;
        AccountType = accountType;

        // Task 4: Step 5a - Initialize the transactions list
    }

    // Copy constructor for BankAccount
    public BankAccount(BankAccount existingAccount)
    {
        Owner = existingAccount.Owner;
        AccountNumber = s_nextAccountNumber++;
        CustomerId = existingAccount.CustomerId;
        Balance = existingAccount.Balance;
        AccountType = existingAccount.AccountType;

        // Task 4: Step 5b - Initialize the transactions list
    }

    // TASK 4: Step 6 - Implement AddTransaction and GetAllTransactions methods

    // Method to deposit money into the account
    public virtual void Deposit(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        if (amount > 0)
        {
            priorBalance = Balance;
            Balance += amount;
            string transactionType = "Deposit";

            if (description.Contains("-(TRANSFER)"))
            {
                transactionType = "Transfer";
            }
            else if (description.Contains("-(BANK REFUND)"))
            {
                transactionType = "Bank Refund";
            }

            // TASK 4: Step 7a - Add logic to log the deposit transaction

        }
    }

    // Method to withdraw money from the account
    public virtual bool Withdraw(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        if (amount > 0 && Balance >= amount)
        {
            priorBalance = Balance;
            Balance -= amount;
            string transactionType = "Withdraw";

            if (description.Contains("-(TRANSFER)"))
            {
                transactionType = "Transfer";
            }
            else if (description.Contains("-(BANK FEE)"))
            {
                transactionType = "Bank Fee";
            }

            // TASK 4: Step 7b - Add logic to log the withdrawal transaction

            return true;
        }

        return false;
    }

    // Method to transfer money to another account
    public virtual bool Transfer(IBankAccount targetAccount, double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        description += "-(TRANSFER)";

        if (Withdraw(amount, transactionDate, transactionTime, description))
        {
            targetAccount.Deposit(amount, transactionDate, transactionTime, description);
            return true;
        }

        return false;
    }

    // Method to apply interest
    public virtual void ApplyInterest(double years, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        priorBalance = Balance;
        double interest = AccountCalculations.CalculateCompoundInterest(Balance, InterestRate, years);
        Balance += interest;

        // TASK 4: Step 7c - Add logic to log the interest transaction
    }

    // Method to issue a cashier's check
    public virtual bool IssueCashiersCheck(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        if (amount > 0 && Balance >= amount + MaxTransactionFee)
        {
            priorBalance = Balance;
            Balance -= amount;
            double fee = AccountCalculations.CalculateTransactionFee(amount, TransactionRate, MaxTransactionFee);
            Balance -= fee;

            // TASK 4: Step 7e - Add logic to log the cashier's check transaction

            return true;
        }

        return false;
    }

    // Method to apply a refund
    public virtual void ApplyRefund(double refund, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        priorBalance = Balance;
        Balance += refund;

        // TASK 4: Step 7d - Add logic to log the refund transaction
    }

    // Method to display account information
    public virtual string DisplayAccountInfo()
    {
        return $"Account Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}, Interest Rate: {InterestRate:P}, Customer ID: {CustomerId}";
    }
}
