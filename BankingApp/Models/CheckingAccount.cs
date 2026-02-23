using System;
using BankingApp.Services;

namespace BankingApp.Models;

public class CheckingAccount : BankAccount
{

    public static double DefaultOverdraftLimit { get; private set; }
    public static double DefaultInterestRate { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    public double OverdraftLimit { get; set; }

    static CheckingAccount()
    {
        DefaultOverdraftLimit = 500;
        DefaultInterestRate = 0.0;
    }

    public CheckingAccount(string customerIdNumber, double balance = 200, double overdraftLimit = 500)
        : base(customerIdNumber, balance, "Checking")
    {
        OverdraftLimit = overdraftLimit;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Overdraft Limit: {OverdraftLimit}";
    }

    public override bool Withdraw(double amount)
    {
        if (amount > 0 && Balance + OverdraftLimit >= amount)
        {
            Balance -= amount;

            if (Balance < 0)
            {
                double overdraftFee = AccountCalculations.CalculateOverdraftFee(Math.Abs(Balance), BankAccount.OverdraftRate, BankAccount.MaxOverdraftFee);
                Balance -= overdraftFee;
                Console.WriteLine($"Overdraft fee of ${overdraftFee} applied.");
            }

            return true;
        }

        return true;
    }
}
