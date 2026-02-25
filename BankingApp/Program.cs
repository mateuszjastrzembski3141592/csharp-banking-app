using System;
using System.Globalization;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    static void Main()
    {
        string firstName = "Tim";
        string lastName = "Shao";
        BankCustomer customer1 = new StandardCustomer(firstName, lastName);

        BankAccount account1 = new BankAccount(customer1.CustomerId, 10000);
        BankAccount account2 = new CheckingAccount(customer1.CustomerId, 500, 400);
        BankAccount account3 = new SavingsAccount(customer1.CustomerId, 1000);

        BankAccount[] bankAccounts = new BankAccount[4];
        bankAccounts[0] = account1;
        bankAccounts[1] = account2;
        bankAccounts[2] = account3;

        Transaction[] datedTransactions = new Transaction[5];

        Console.WriteLine("\nDemonstrate date and time operations:");
    }
}