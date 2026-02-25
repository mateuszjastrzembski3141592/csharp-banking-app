using System;

namespace BankingApp.Models;

public class PremiumCustomer : BankCustomer
{
    private const decimal MinimumCombinedBalance = 100000;

    public PremiumCustomer(string firstName, string lastName)
        : base(firstName, lastName)
    {
    }

    public override bool IsPremiumCustomer()
    {
        if (GetCombinedBalance() >= MinimumCombinedBalance && HasRequiredAccountTypes())
        {
            return true;
        }

        return false;
    }

    public override void ApplyBenefits()
    {
        if (IsPremiumCustomer())
        {
            Console.WriteLine("Congratulations! Your premium customer benefits include:");
            Console.WriteLine(" - Dedicated customer service line");
            Console.WriteLine(" - Free overdraft protection for your checking account");
            Console.WriteLine(" - Higher interest rates on savings accounts");
            Console.WriteLine(" - Free wire transfers and cashier's checks");
            Console.WriteLine(" - Free safe deposit box rental");
        }
        else
        {
            Console.WriteLine("See a manager to learn about our premium accounts.");
        }
    }

    private bool HasRequiredAccountTypes()
    {
        return true;
    }

    private decimal GetCombinedBalance()
    {
        decimal combinedBalance = 200000;
        return combinedBalance;
    }
}
