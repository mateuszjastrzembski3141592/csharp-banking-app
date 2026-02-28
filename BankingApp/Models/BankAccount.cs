using System;
using BankingApp.Interfaces;
using BankingApp.Services;

namespace BankingApp.Models;

public class BankAccount : IBankAccount
{
    private static int s_nextAccountNumber;
    protected double priorBalance;

    private readonly List<Transaction> _transactions;

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

    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

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
        _transactions = [];
    }

    // Copy constructor for BankAccount
    public BankAccount(BankAccount existingAccount)
    {
        Owner = existingAccount.Owner;
        AccountNumber = s_nextAccountNumber++;
        CustomerId = existingAccount.CustomerId;
        Balance = existingAccount.Balance;
        AccountType = existingAccount.AccountType;
        _transactions = [.. existingAccount._transactions];
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    // Method to return all transactions for the account
    public List<Transaction> GetAllTransactions()
    {
        return _transactions;
    }

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

            AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, amount, AccountNumber, AccountNumber, transactionType, description));
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

            AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, amount, AccountNumber, AccountNumber, transactionType, description));

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

        AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, interest, AccountNumber, AccountNumber, AccountType, "Interest"));
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

            AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, amount, AccountNumber, AccountNumber, AccountType, "Cashier's Check"));
            AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, fee, AccountNumber, AccountNumber, AccountType, "Transaction Fee"));

            return true;
        }

        return false;
    }

    // Method to apply a refund
    public virtual void ApplyRefund(double refund, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        priorBalance = Balance;
        Balance += refund;

        AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, refund, AccountNumber, AccountNumber, AccountType, "Refund"));
    }

    // Method to display account information
    public virtual string DisplayAccountInfo()
    {
        return $"Account Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance:C}, Interest Rate: {InterestRate:P}, Customer ID: {CustomerId}";
    }
}
