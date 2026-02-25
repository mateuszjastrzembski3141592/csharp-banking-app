using System;

namespace BankingApp.Services;

public static class AccountCalculations
{
    // Method to calculcate compound interest
    public static double CalculateCompoundInterest(double principal, double annualRate, double years)
    {
        return principal * Math.Pow(1 + annualRate, years) - principal;
    }

    // Method to validate account number
    public static bool ValidateAccountNumber(int accountNumber)
    {
        return accountNumber.ToString().Length == 8;
    }

    // Method to calculate transaction fee
    public static double CalculateTransactionFee(double amount, double transactionRate, double maxTransactionFee)
    {
        // Fee based on the transaction rate
        double fee = amount * transactionRate;
        return Math.Min(fee, maxTransactionFee);
    }

    // Method to calculate overdraft fee
    public static double CalculateOverdraftFee(double amountOverdrawn, double overdraftRate, double maxOverdraftFee)
    {
        // Fee based on the overdraft rate
        double fee = amountOverdrawn * overdraftRate;
        return Math.Min(fee, maxOverdraftFee);
    }

    // Method to calculate currency value after exchange
    public static double ReturnForeignCurrency(double amount, double exchangeRate)
    {
        return amount * exchangeRate;
    }
}
