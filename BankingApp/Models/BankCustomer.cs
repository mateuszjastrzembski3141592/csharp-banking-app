using System;
using System.Collections.Generic;
using BankingApp.Interfaces;

namespace BankingApp.Models;

public abstract partial class BankCustomer : IBankCustomer
{
    private static int s_nextCustomerId;
    private string _firstName = "Tim";
    private string _lastName = "Shao";

    // TASK 3: Step 3 - declare the Accounts field

    public string CustomerId { get; }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    // TASK 3: Step 4 - expose the Accounts property

    static BankCustomer()
    {
        Random random = new();
        s_nextCustomerId = random.Next(10000000, 20000000);
    }

    public BankCustomer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        CustomerId = s_nextCustomerId++.ToString("D10");

        // TASK 3: Step 5 - initialize the Accounts field
    }

    // Copy constructor for BankCustomer
    public BankCustomer(BankCustomer existingCustomer)
    {
        FirstName = existingCustomer.FirstName;
        LastName = existingCustomer.LastName;
        CustomerId = s_nextCustomerId++.ToString("D10");

        // TASK 3: Step 6 - initialize the Accounts field by copying from existingCustomer
    }
}
