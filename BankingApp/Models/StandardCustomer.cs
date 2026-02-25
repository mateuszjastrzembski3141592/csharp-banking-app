using System;

namespace BankingApp.Models;

public class StandardCustomer : BankCustomer
{
    public StandardCustomer(string firstName, string lastName) : base(firstName, lastName)
    {
    }

    public override bool IsPremiumCustomer() => false;

    public override void ApplyBenefits()
    {
        // No additional benefits for StandardCustomer
    }
}
