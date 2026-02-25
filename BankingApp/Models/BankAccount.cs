using System;
using BankingApp.Interfaces;
using BankingApp.Services;

namespace BankingApp.Models;

public class BankAccount : IBankAccount
{
    private static int s_nextAccountNumber;

    public static double TransactionRate { get; private set; }
    public static double MaxTransactionFee { get; private set; }
    public static double OverdraftRate { get; private set; }
    public static double MaxOverdraftFee { get; private set; }

    public int AccountNumber { get; }
    public string CustomerId { get; }
    public double Balance { get; internal set; } = 0;
    public string AccountType { get; set; } = "Checking";

    public virtual double InterestRate { get; protected set; }

    static BankAccount()
    {
        Random random = new();
        s_nextAccountNumber = random.Next(10000000, 20000000);
        TransactionRate = 0.01;
        MaxTransactionFee = 10;
        OverdraftRate = 0.05;
        MaxOverdraftFee = 10;
    }

    public BankAccount(string customerIdNumber, double balance = 200, string accountType = "Checking")
    {
        AccountNumber = s_nextAccountNumber++;
        CustomerId = customerIdNumber;
        Balance = balance;
        AccountType = accountType;
    }

    // Copy constructor for BankAccount
    public BankAccount(BankAccount existingAccount)
    {
        AccountNumber = s_nextAccountNumber++;
        CustomerId = existingAccount.CustomerId;
        Balance = existingAccount.Balance;
        AccountType = existingAccount.AccountType;
    }

    // Method to deposit money into the account
    public virtual void Deposit(double amount)
    {
        if (amount > 0)
        {
            Balance += amount;
        }
    }

    // Method to withdraw money from the account
    public virtual bool Withdraw(double amount)
    {
        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount;
            return true;
        }

        return false;
    }

    // Method to transfer money to another account
    public virtual bool Transfer(IBankAccount targetAccount, double amount)
    {
        if (Withdraw(amount))
        {
            targetAccount.Deposit(amount);
            return true;
        }

        return false;
    }

    // Method to apply interest
    public virtual void ApplyInterest(double years)
    {
        Balance += AccountCalculations.CalculateCompoundInterest(Balance, InterestRate, years);
    }

    // Method to issue a cashier's check
    public virtual bool IssueCashiersCheck(double amount)
    {
        if (amount > 0 && Balance >= amount + MaxTransactionFee)
        {
            Balance -= amount;
            Balance -= AccountCalculations.CalculateTransactionFee(amount, TransactionRate, MaxTransactionFee);
            return true;
        }

        return false;
    }

    // Method to apply a refund
    public virtual void ApplyRefund(double refund)
    {
        Balance += refund;
    }

    // Method to display account information
    public virtual string DisplayAccountInfo()
    {
        return $"Account Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}, Interest Rate: {InterestRate:P}, Customer ID: {CustomerId}";
    }
}

// TODO: Potential change - mark BankAccount as abstract.