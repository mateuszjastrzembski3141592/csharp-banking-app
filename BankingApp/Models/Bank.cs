using System;

namespace BankingApp.Models;

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

    internal IEnumerable<BankCustomer> GetAllCustomers()
    {
        return [.. _customers];
    }

    internal IEnumerable<BankCustomer> GetCustomersByName(string firstName, string lastName)
    {
        List<BankCustomer> matchingCustomers = [];

        foreach (BankCustomer customer in _customers)
        {
            if (customer.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                customer.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
            {
                matchingCustomers.Add(customer);
            }
        }

        return matchingCustomers;
    }

    internal BankCustomer? GetCustomerById(string customerId)
    {
        return _customers.FirstOrDefault(customer => customer.CustomerId.Equals(customerId, StringComparison.OrdinalIgnoreCase));
    }

    internal int GetNumberOfTransactions()
    {
        int totalTransactions = 0;

        foreach (BankCustomer customer in _customers)
        {
            foreach (BankAccount account in customer.Accounts.Cast<BankAccount>())
            {
                foreach (Transaction transaction in account.Transactions)
                {
                    totalTransactions++;
                }
            }
        }

        return totalTransactions;
    }

    internal double GetTotalMoneyInVault()
    {
        double totalBankCash = 0;

        foreach (BankCustomer customer in _customers)
        {
            foreach (BankAccount account in customer.Accounts.Cast<BankAccount>())
            {
                totalBankCash += account.Balance;
            }
        }

        return totalBankCash;
    }

    internal double GetDailyDeposits(DateOnly date)
    {
        double totalDailyDeposits = 0;

        foreach (BankCustomer customer in _customers)
        {
            foreach (BankAccount account in customer.Accounts.Cast<BankAccount>())
            {
                foreach (Transaction transaction in account.Transactions)
                {
                    if (transaction.TransactionDate == date && transaction.TransactionType == "Deposit")
                    {
                        totalDailyDeposits += transaction.TransactionAmount;
                    }
                }
            }
        }

        return totalDailyDeposits;
    }

    internal double GetDailyWithdrawals(DateOnly date)
    {
        double totalDailyWithdrawals = 0;

        foreach (BankCustomer customer in _customers)
        {
            foreach (BankAccount account in customer.Accounts.Cast<BankAccount>())
            {
                foreach (Transaction transaction in account.Transactions)
                {
                    if (transaction.TransactionDate == date && transaction.TransactionType == "Withdraw")
                    {
                        totalDailyWithdrawals += transaction.TransactionAmount;
                    }
                }
            }
        }

        return totalDailyWithdrawals;
    }

    internal void AddCustomer(BankCustomer customer)
    {
        _customers.Add(customer);
    }

    internal void RemoveCustomer(BankCustomer customer)
    {
        _customers.Remove(customer);
    }

    internal void AddCustomer(IEnumerable<BankCustomer> customers)
    {
        _customers.AddRange(customers);
    }
}
