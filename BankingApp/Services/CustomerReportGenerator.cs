using System;
using BankingApp.Interfaces;


namespace BankingApp.Services;

public class CustomerReportGenerator : IMonthlyReportGenerator, IQuarterlyReportGenerator, IYearlyReportGenerator
{
    private readonly IBankCustomer _customer;

    public CustomerReportGenerator(IBankCustomer customer)
    {
        _customer = customer;
    }

    public void GenerateMonthlyReport()
    {
        Console.WriteLine($"Generating monthly report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateCurrentMonthToDateReport()
    {
        Console.WriteLine($"Generating current month-to-date report for customer: {_customer.ReturnFullName()}");
    }

    public void GeneratePrevious30DayReport()
    {
        Console.WriteLine($"Generating previous month report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateQuarterlyReport()
    {
        Console.WriteLine($"Generating quarterly report for customer: {_customer.ReturnFullName()}");
    }

    public void GeneratePreviousYearReport()
    {
        Console.WriteLine($"Generating previous year report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateCurrentYearToDateReport()
    {
        Console.WriteLine($"Generating current year-to-date report for customer: {_customer.ReturnFullName()}");
    }

    public void GenerateLast365DaysReport()
    {
        Console.WriteLine($"Generating last 365 days report for customer: {_customer.ReturnFullName()}");
    }
}
