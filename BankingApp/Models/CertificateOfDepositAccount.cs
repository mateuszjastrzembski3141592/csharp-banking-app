using System;

namespace BankingApp.Models;

public class CertificateOfDepositAccount : BankAccount
{
    public DateTime MaturityDate { get; set; }
    public double EarlyWithdrawalPenalty { get; set; }

    public static double DefaultInterestRate { get; private set; }
    public static double LongTermInterestRate { get; private set; }
    public static int DefaultTermInMonths { get; private set; }

    public override double InterestRate
    {
        get { return DefaultInterestRate; }
        protected set { DefaultInterestRate = value; }
    }

    static CertificateOfDepositAccount()
    {
        DefaultInterestRate = 0.05;
        LongTermInterestRate = 0.0425;
        DefaultTermInMonths = 6;
    }

    public CertificateOfDepositAccount(BankCustomer owner, string customerIdNumber, double balance = 1000, int termInMonths = 6, double earlyWithdrawalPenalty = 0.1)
        : base(owner, customerIdNumber, balance, "Certificate of Deposit")
    {
        if (termInMonths != 6 && termInMonths != 12)
        {
            throw new ArgumentException("Term must ne either 6 months or 12 months");
        }

        MaturityDate = DateTime.Now.AddMonths(termInMonths);
        EarlyWithdrawalPenalty = earlyWithdrawalPenalty;
        //InterestRate = (termInMonths == 12) ? LongTermInterestRate : DefaultInterestRate; // Set the interest rate based on the term
    }

    public override bool Withdraw(double amount, DateOnly transactionDate, TimeOnly transactionTime, string description)
    {
        if (DateTime.Now < MaturityDate)
        {
            amount += amount * EarlyWithdrawalPenalty;
        }

        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }

    public override string DisplayAccountInfo()
    {
        return base.DisplayAccountInfo() + $", Maturity Date: {MaturityDate:d}, Early Withdrawal Penalty: {EarlyWithdrawalPenalty * 100}%, Interest Rate: {InterestRate * 100}%";
    }
}
