using System;
using BankingApp.Models;

namespace BankingApp.Interfaces;

public interface IQuarterlyReportGenerator
{
    void GenerateQuarterlyReport(Transaction[] transactions, DateOnly reportDate);
}
