using System;

namespace BankingApp.Interfaces;

public interface IMonthlyReportGenerator
{
    void GenerateMonthlyReport();
    void GenerateCurrentMonthToDateReport();
    void GeneratePrevious30DayReport();
}
