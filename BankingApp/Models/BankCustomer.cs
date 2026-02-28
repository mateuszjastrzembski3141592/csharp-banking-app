using System;
using System.Collections.Generic;
using BankingApp.Interfaces;

namespace BankingApp.Models;

public abstract partial class BankCustomer : IBankCustomer
{
    private static int s_nextCustomerId;
    private string _firstName = "Tim";
    private string _lastName = "Shao";

    private readonly List<IBankAccount> _accounts;

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

    public IReadOnlyList<IBankAccount> Accounts => _accounts.AsReadOnly();

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

        _accounts = [];
    }

    // Copy constructor for BankCustomer
    public BankCustomer(BankCustomer existingCustomer)
    {
        FirstName = existingCustomer.FirstName;
        LastName = existingCustomer.LastName;
        CustomerId = s_nextCustomerId++.ToString("D10");

        _accounts = [.. existingCustomer._accounts];
    }
}
