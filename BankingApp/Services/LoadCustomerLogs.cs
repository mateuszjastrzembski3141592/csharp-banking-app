using System;
using System.IO;
using System.Text.Json;
using BankingApp.Models;

namespace BankingApp.Services;

public class LoadCustomerLogs
{
    // create the GenerateCustomerData method
    public static void ReadCustomerData(Bank bank)
    {
        string ConfigDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Config");
        string accountsDirectoryPath = Path.Combine(ConfigDirectoryPath, "Accounts");
        string transactionsDirectoryPath = Path.Combine(ConfigDirectoryPath, "Transactions");

        JsonRetrieval.LoadAllCustomers(bank, ConfigDirectoryPath, accountsDirectoryPath, transactionsDirectoryPath);
    }
}
