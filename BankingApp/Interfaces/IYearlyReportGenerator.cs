using System;
using BankingApp.Models;

namespace BankingApp.Interfaces;

public interface IYearlyReportGenerator
{
    void GeneratePreviousYearReport(Transaction[] transactions, DateOnly reportDate);
    void GenerateCurrentYearToDateReport(Transaction[] transactions, DateOnly reportDate);
    void GenerateLast365DaysReport(Transaction[] transactions, DateOnly reportDate);
}
