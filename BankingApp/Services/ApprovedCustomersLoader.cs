using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BankingApp.Services;

public static class ApprovedCustomersLoader
{
    private static readonly string ConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "ApprovedCustomers.json");

    public static List<ApprovedCustomer> LoadApprovedName()
    {
        if (!File.Exists(ConfigFilePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {ConfigFilePath}");
        }

        var json = File.ReadAllText(ConfigFilePath);
        var config = JsonSerializer.Deserialize<ApprovedCustomersConfig>(json);

        return config?.ApprovedNames ?? [];
    }

    private class ApprovedCustomersConfig
    {
        public List<ApprovedCustomer> ApprovedNames { get; set; } = [];
    }

    public class ApprovedCustomer
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
