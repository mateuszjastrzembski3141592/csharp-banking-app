using System;

namespace BankingApp.Interfaces;

public interface IBankCustomer
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string CustomerId { get; }

    // TASK 3: Step 1 - Expose Accounts property

    string ReturnFullName();
    void UpdateName(string firstName, string lastName);
    string DisplayCustomerInfo();
    bool IsPremiumCustomer();
    void ApplyBenefits();

    // TASK 3: Step 2 - Add account-management methods 
}
