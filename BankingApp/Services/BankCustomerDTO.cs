using System;
using BankingApp.Models;

namespace BankingApp.Services;

public class BankCustomerDTO
{
    public string CustomerId { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public List<int> AccountNumbers { get; set; } = [];

    public static BankCustomerDTO MapToDTO(BankCustomer bankCustomer)
    {
        return new BankCustomerDTO
        {
            CustomerId = bankCustomer.CustomerId,
            FirstName = bankCustomer.FirstName,
            LastName = bankCustomer.LastName,
            AccountNumbers = [.. bankCustomer.Accounts.Select(a => a.AccountNumber)]
        };
    }
}
