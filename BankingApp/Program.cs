using System;
using System.Globalization;
using System.Collections.Generic;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine($"Creating Bank, BankCustomer, and BankAccount objects...");

        Bank bank = new();
        Console.WriteLine($"\nBank object created...");

        BankCustomer customer1 = new StandardCustomer("Tim", "Shao");
        bank.AddCustomer(customer1);
        Console.WriteLine($"Bank customer {customer1.ReturnFullName()} created and added to the Bank object's customer collection...");

        double openingBalanceChecking = 5000.00;
        double openingBalanceSavings = 20000.00;
        BankAccount account1 = new CheckingAccount(customer1, customer1.CustomerId, openingBalanceChecking);
        BankAccount account2 = new SavingsAccount(customer1, customer1.CustomerId, openingBalanceSavings);
        customer1.AddAccount(account1);
        customer1.AddAccount(account2);
        Console.WriteLine($"{customer1.Accounts[0].AccountType} and {customer1.Accounts[1].AccountType} account objects created and added to {customer1.ReturnFullName()}'s account collection...");

        Console.WriteLine($"\nIs {customer1.ReturnFullName()} a premium customer: {customer1.IsPremiumCustomer()}");

        Console.WriteLine("Account details:");

        foreach (var account in customer1.Accounts)
        {
            Console.WriteLine($"  - {account.DisplayAccountInfo()}");
        }

        double openingBalanceMoneyMarket = 90000.00;
        Console.WriteLine($"\nOpen a MoneyMarket account for {customer1.ReturnFullName()} with a {openingBalanceMoneyMarket:C} balance and check premium status...");
        BankAccount account3 = new MoneyMarketAccount(customer1, customer1.CustomerId, openingBalanceMoneyMarket);
        customer1.AddAccount(account3);

        Console.WriteLine($"Is {customer1.ReturnFullName()} a premium customer: {customer1.IsPremiumCustomer()}");

        Console.WriteLine("Account details:");

        foreach (var account in customer1.Accounts)
        {
            Console.WriteLine($"  - {account.DisplayAccountInfo()}");
        }
    }
}

