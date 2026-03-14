using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.Json;
using BankingApp.Interfaces;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Create a performance baseline by loading data synchronously.");

        // Create Bank objects
        Bank bank1 = new(); // Bank object to load data synchronously
        Bank bank2 = new(); // Bank object to load data asynchronously
        Bank bank3 = new(); // Bank object to load data asynchronously and in parallel

        // Get the time before loading the data
        DateTime timeBeforeLoadCall = DateTime.Now;

        // Load the customer data from the file
        LoadCustomerLogs.ReadCustomerData(bank1);

        // Get the time after loading the data
        DateTime timeAfterLoadCall = DateTime.Now;

        // Calculate the time taken to load the data
        TimeSpan timeTakenToReturn = timeAfterLoadCall - timeBeforeLoadCall;
        Console.WriteLine($"\nPerformance baseline: time taken to return to Main: {timeTakenToReturn.TotalSeconds} seconds");

        await Task.Delay(2000);

        // Load the customer data asynchronously from the file
        Console.WriteLine("\nImplement File I/O tasks asynchronously.");

        // Get the time before loading the data asynchronously
        DateTime timeBeforeAsyncLoadCall = DateTime.Now;

        // Start the async data loading task
        var asyncLoadTask = LoadCustomerLogsAsync.ReadCustomerDataAsync(bank2);

        DateTime timeAfterAsyncLoadCall = DateTime.Now;

        Console.WriteLine($"\nTime taken to return to Main: {(timeAfterAsyncLoadCall - timeBeforeAsyncLoadCall).TotalSeconds} seconds");

        // Wait for the async task to complete
        await asyncLoadTask;

        DateTime timeAfterAsyncLoadCompleted = DateTime.Now;

        Console.WriteLine($"Time taken to load the data asynchronously: {(timeAfterAsyncLoadCompleted - timeBeforeAsyncLoadCall).TotalSeconds} seconds");

        await Task.Delay(2000);

        Console.WriteLine("\nImplement File I/O tasks asynchronously and in parallel.");

        // Get the time before loading the data asynchronously using parallel tasks
        DateTime timeBeforeAsyncParallelLoadCall = DateTime.Now;

        // Start the async data loading task
        var asyncParallelLoadTask = LoadCustomerLogsAsyncParallel.ReadCustomerDataAsyncParallel(bank3);

        DateTime timeAfterAsyncParallelLoadCall = DateTime.Now;

        // Wait for the async task to complete
        await asyncParallelLoadTask;

        DateTime timeAfterAsyncParallelLoadCompleted = DateTime.Now;

        Console.WriteLine($"\nTime taken to return to Main: {(timeAfterAsyncParallelLoadCall - timeBeforeAsyncParallelLoadCall).TotalSeconds} seconds");
        Console.WriteLine($"Time taken to load the data asynchronously and in parallel: {(timeAfterAsyncParallelLoadCompleted - timeBeforeAsyncParallelLoadCall).TotalSeconds} seconds");

    }
}
