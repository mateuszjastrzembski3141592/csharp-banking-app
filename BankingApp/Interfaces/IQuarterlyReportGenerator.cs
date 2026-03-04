using System;
using BankingApp.Models;

namespace BankingApp.Interfaces;

public interface IQuarterlyReportGenerator
{
    void GenerateQuarterlyReport(BankCustomer bankCustomer, int accountNumber, DateOnly reportDate);
}
