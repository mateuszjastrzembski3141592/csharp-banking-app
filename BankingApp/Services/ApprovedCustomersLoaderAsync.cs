using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankingApp.Services;

public class ApprovedCustomersLoaderAsync
{
    private static readonly string ConfigFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "ApprovedCustomers.json");

    public static async Task<List<ApprovedCustomer>> LoadApprovedNamesAsync()
    {
        if (!File.Exists(ConfigFilePath))
        {
            throw new FileNotFoundException($"Configuration file not found: {ConfigFilePath}");
        }

        var json = await File.ReadAllTextAsync(ConfigFilePath);
        var config = await JsonSerializer.DeserializeAsync<ApprovedCustomersConfig>(new MemoryStream(Encoding.UTF8.GetBytes(json)));

        return config?.ApprovedNames ?? [];
    }

    private class ApprovedCustomersConfig
    {
        public List<ApprovedCustomer> ApprovedNames { get; set; } = new List<ApprovedCustomer>();
    }

    public class ApprovedCustomer
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}