using System;

namespace BankingApp.Models;

// TASK 2: Create Bank Class
// Purpose: Manage customers and transaction reports.

public class Bank
{
    private readonly Guid _bankId;
    private readonly List<BankCustomer> _customers;

    public Guid BankId => _bankId;
    public IReadOnlyList<BankCustomer> Customers => _customers.AsReadOnly();

    public Bank()
    {
        _bankId = Guid.NewGuid();
        _customers = [];
    }

    internal void AddCustomer(BankCustomer customer)
    {
        _customers.Add(customer);
    }
}
