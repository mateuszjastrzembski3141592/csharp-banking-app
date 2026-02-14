using System;

namespace BankingApp;

public class MoneyMarketAccount : BankAccount
{

    public static double DefaultMinimumBalance { get; private set; }
    public static double DefaultMinimumOpeningBalance { get; private set; }
    public static double DefaultInterestRate { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    public double MinimumBalance { get; set; }
    public double MinimumOpeningBalance { get; set; }


    static MoneyMarketAccount()
    {
        DefaultMinimumBalance = 1000;
        DefaultMinimumOpeningBalance = 2000;
        DefaultInterestRate = 0.04;
    }

    public MoneyMarketAccount(string customerIdNumber, double balance = 2000, double minimumBalance = 1000)
        : base(customerIdNumber, balance, "Money Market")
    {
        if (balance < DefaultMinimumOpeningBalance)
        {
            throw new ArgumentException($"Balance must be at least {DefaultMinimumOpeningBalance}");
        }

        MinimumBalance = minimumBalance;
        MinimumOpeningBalance = DefaultMinimumOpeningBalance;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Minimum Balance: {MinimumBalance}, Minimum Opening Balance: {MinimumOpeningBalance}";
    }

    public override bool Withdraw(double amount)
    {
        if (amount > 0 && Balance - amount >= MinimumBalance)
        {
            Balance -= amount;
            return true;
        }

        return true;
    }
}
