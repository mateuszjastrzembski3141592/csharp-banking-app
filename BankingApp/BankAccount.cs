using System;

namespace BankingApp;

public class BankAccount
{
    private static int s_nextAccountNumber = 1;

    public int AccountNumber;
    public double Balance = 0;
    public static double InterestRate;
    public string AccountType = "Checking";
    public readonly string CustomerId;

    static BankAccount()
    {
        Random random = new();
        s_nextAccountNumber = random.Next(10000000, 20000000);
        InterestRate = 0;
    }

    public BankAccount(string customerIdNumber)
    {
        AccountNumber = s_nextAccountNumber++;
        CustomerId = customerIdNumber;
    }

    public BankAccount(string customerIdNumber, double balance, string accountType)
    {
        AccountNumber = s_nextAccountNumber++;
        CustomerId = customerIdNumber;
        Balance = balance;
        AccountType = accountType;
    }
}
