using System;

namespace BankingApp.Models;

public class SavingsAccount : BankAccount
{
    private int _withdrawalsThisMonth;

    public int WithdrawalLimit { get; set; }
    public double MinimumOpeningBalance { get; set; }

    public static int DefaultWithdrawalLimit { get; private set; }
    public static double DefaultMinimumOpeningBalance { get; private set; }
    public static double DefaultInterestRate { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    static SavingsAccount()
    {
        DefaultWithdrawalLimit = 6;
        DefaultMinimumOpeningBalance = 500;
        DefaultInterestRate = 0.02;
    }

    public SavingsAccount(BankCustomer owner, string customerIdNumber, double balance = 500, int withdrawalLimit = 6)
        : base(owner, customerIdNumber, balance, "Savings")
    {
        if (Balance < DefaultMinimumOpeningBalance)
        {
            throw new ArgumentException($"Balance must be at least {DefaultMinimumOpeningBalance}");
        }

        WithdrawalLimit = withdrawalLimit;
        _withdrawalsThisMonth = 0;
        MinimumOpeningBalance = DefaultMinimumOpeningBalance;
    }

    public override bool Withdraw(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        if (amount > 0 && Balance >= amount && _withdrawalsThisMonth < WithdrawalLimit)
        {
            bool result = base.Withdraw(amount, transactionDate, transactionTime, description);
            _withdrawalsThisMonth++;
            return result;
        }

        return false;
    }

    public void ResetWithdrawalLimit()
    {
        _withdrawalsThisMonth = 0;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Withdrawal Limit: {WithdrawalLimit},Withdrawals This Month: {_withdrawalsThisMonth}";
    }
}
