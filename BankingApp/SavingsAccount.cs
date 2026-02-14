using System;

namespace BankingApp;

public class SavingsAccount : BankAccount
{
    private int _withdrawalsThisMonth;

    public static int DefaultWithdrawalLimit { get; private set; }
    public static double DefaultMinimumOpeningBalance { get; private set; }
    public static double DefaultInterestRate { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    public int WithdrawalLimit { get; set; }
    public double MinimumOpeningBalance { get; set; }

    static SavingsAccount()
    {
        DefaultWithdrawalLimit = 6;
        DefaultMinimumOpeningBalance = 500;
        DefaultInterestRate = 0.02;
    }

    public SavingsAccount(string customerIdNumber, double balance = 500, int withdrawalLimit = 6)
        : base(customerIdNumber, balance, "Savings")
    {
        if (Balance < DefaultMinimumOpeningBalance)
        {
            throw new ArgumentException($"Balance must be at least {DefaultMinimumOpeningBalance}");
        }

        WithdrawalLimit = withdrawalLimit;
        _withdrawalsThisMonth = 0;
        MinimumOpeningBalance = DefaultMinimumOpeningBalance;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Withdrawal Limit: {WithdrawalLimit},Withdrawals This Month: {_withdrawalsThisMonth}";
    }

    public override bool Withdraw(double amount)
    {
        if (amount > 0 && Balance >= amount && _withdrawalsThisMonth < WithdrawalLimit)
        {
            Balance -= amount;
            _withdrawalsThisMonth++;
            return true;
        }

        return true;
    }

    public void ResetWithdrawalLimit()
    {
        _withdrawalsThisMonth = 0;
    }
}
