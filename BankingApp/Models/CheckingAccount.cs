using System;
using BankingApp.Services;

namespace BankingApp.Models;

public class CheckingAccount : BankAccount
{
    public double OverdraftLimit { get; set; }

    public static double DefaultOverdraftLimit { get; private set; }
    public static double DefaultInterestRate { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    static CheckingAccount()
    {
        DefaultOverdraftLimit = 500;
        DefaultInterestRate = 0.0;
    }

    public CheckingAccount(BankCustomer owner, string customerIdNumber, double balance = 200, double overdraftLimit = 500)
        : base(owner, customerIdNumber, balance, "Checking")
    {
        OverdraftLimit = overdraftLimit;
    }

    public override bool Withdraw(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        bool result = base.Withdraw(amount, transactionDate, transactionTime, description);

        if (result == false && !description.Contains("-(TRANSFER)"))
        {
            if (Owner.IsPremiumCustomer() == true)
            {
                priorBalance = Balance;
                Balance -= amount;

                string transactionType = "Withdraw";
                AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, amount, AccountNumber, AccountNumber, transactionType, description));

                BankAccount savingsAccount = (SavingsAccount)Owner.Accounts[1];
                BankAccount checkingAccount = (CheckingAccount)Owner.Accounts[0];

                string transferDescription = "free overdraft protection-(TRANSFER)";
                savingsAccount.Transfer(checkingAccount, amount + 1000.00, transactionDate, transactionTime, transferDescription);

                return true;
            }
            else
            {
                double overdraftFee = AccountCalculations.CalculateOverdraftFee(Math.Abs(Balance), OverdraftRate, MaxOverdraftFee);

                if (Balance + OverdraftLimit + overdraftFee >= amount)
                {
                    priorBalance = Balance;
                    Balance -= amount;

                    string transactionType = "Withdraw";
                    AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, amount, AccountNumber, AccountNumber, transactionType, description));

                    priorBalance = Balance;
                    Balance -= overdraftFee;
                    
                    transactionType = "Bank Fee";
                    string overdraftDescription = "Overdraft fee applied";
                    AddTransaction(new Transaction(transactionDate, transactionTime, priorBalance, overdraftFee, AccountNumber, AccountNumber, transactionType, overdraftDescription));

                    return true;
                }
            }
        }

        return result;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Overdraft Limit: {OverdraftLimit}";
    }
}
