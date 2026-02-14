using System;
using System.Globalization;

namespace BankingApp;

class Program
{
    static void Main()
    {
        // Create BankCustomer objects
        string firstName = "Tim";
        string lastName = "Shao";

        Console.WriteLine($"Creating a BankCustomer object for customer {firstName} {lastName}...");
        BankCustomer customer1 = new(firstName, lastName);

        // Demonstrate instantiating derived classes
        Console.WriteLine($"\nUsing derived classes to create bank account objects for {customer1.ReturnFullName()}...");

        // BankAccount bankAccount1 = new(customer1.CustomerId, 1000, "Checking");
        CheckingAccount checkingAccount1 = new(customer1.CustomerId, 500);
        SavingsAccount savingsAccount1 = new(customer1.CustomerId, 1000);
        MoneyMarketAccount moneyMarketAccount1 = new(customer1.CustomerId, 2000);

        // Demonstrate using inherited properties
        Console.WriteLine($"\nUsing inherited properties to display {customer1.ReturnFullName()}'s account information...");

        // Console.WriteLine($" - {bankAccount1.AccountType} account #{bankAccount1.AccountNumber} has a balance of {bankAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine($" - {checkingAccount1.AccountType} account #{checkingAccount1.AccountNumber} has a balance of {checkingAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine($" - {savingsAccount1.AccountType} account #{savingsAccount1.AccountNumber} has a balance of {savingsAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine($" - {moneyMarketAccount1.AccountType} account #{moneyMarketAccount1.AccountNumber} has a balance of {moneyMarketAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");

        // Demonstrate using inherited methods
        double transactionAmount = 200;

        Console.WriteLine("\nDemonstrating inheritance of the Withdraw, Transfer, and Deposit methods from the base class...");

        // Withdraw from checking account
        Console.WriteLine($" - Withdraw {transactionAmount} from {checkingAccount1.AccountType} account");
        checkingAccount1.Withdraw(transactionAmount);
        Console.WriteLine($" - {checkingAccount1.AccountType} account #{checkingAccount1.AccountNumber} has a balance of {checkingAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");

        // Transfer from savings account to checking account
        Console.WriteLine($" - Transfer {transactionAmount.ToString("C", CultureInfo.CurrentCulture)} from {savingsAccount1.AccountType} account into {checkingAccount1.AccountType} account");
        savingsAccount1.Transfer(checkingAccount1, transactionAmount);
        Console.WriteLine($" - {savingsAccount1.AccountType} account #{savingsAccount1.AccountNumber} has a balance of {savingsAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine($" - {checkingAccount1.AccountType} account #{checkingAccount1.AccountNumber} has a balance of {checkingAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");
        
        // Deposit into money making account
        Console.WriteLine($" - Deposit {transactionAmount.ToString("C", CultureInfo.CurrentCulture)} into {moneyMarketAccount1.AccountType} account");
        moneyMarketAccount1.Deposit(transactionAmount);
        Console.WriteLine($" - {moneyMarketAccount1.AccountType} account #{moneyMarketAccount1.AccountNumber} has a balance of {moneyMarketAccount1.Balance.ToString("C", CultureInfo.CurrentCulture)}");

        // Demonstrate using the `new` keyword to override a base class method
        Console.WriteLine("\nDemonstrating the use of the `new` keyword to override a base class method...");

        Console.WriteLine($" - {checkingAccount1.DisplayAccountInfo()}");
        Console.WriteLine($" - {savingsAccount1.DisplayAccountInfo()}");
        Console.WriteLine($" - {moneyMarketAccount1.DisplayAccountInfo()}");

        // Demonstrate the overridden properties and methods
        Console.WriteLine("\nDemonstrating specialized Withdraw behavior:");

        // CheckingAccount: Withdraw within overdraft limit
        Console.WriteLine("\nCheckingAccount: Attempting to withdraw $600 (within overdraft limit)...");
        checkingAccount1.Withdraw(600);
        Console.WriteLine(checkingAccount1.DisplayAccountInfo());

        // CheckingAccount: Withdraw exceeding overdraft limit
        Console.WriteLine("\nCheckingAccount: Attempting to withdraw $1000 (exceeding overdraft limit)...");
        checkingAccount1.Withdraw(1000);
        Console.WriteLine(checkingAccount1.DisplayAccountInfo());

        // SavingsAccount: Withdraw within limit
        Console.WriteLine("\nSavingsAccount: Attempting to withdraw $200 (within withdraw limit)...");
        savingsAccount1.Withdraw(200);
        Console.WriteLine(savingsAccount1.DisplayAccountInfo());

        // SavingsAccount: Withdraw exceeding limit
        Console.WriteLine("\nSavingsAccount: Attempting to withdraw $900 (exceeding withdraw limit)...");
        savingsAccount1.Withdraw(900);
        Console.WriteLine(savingsAccount1.DisplayAccountInfo());

        // SavingsAccount: Exceed maximum number of withdrawals per month
        Console.WriteLine("\nSavingsAccount: Exceeding maximum number of withdrawals per month");
        savingsAccount1.ResetWithdrawalLimit();

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"Attempting to withdraw $50 (withdrawal {i + 1})...");
            savingsAccount1.Withdraw(50);
            Console.WriteLine(savingsAccount1.DisplayAccountInfo());
        }

        // MoneyMarketAccount: Withdraw within minimum balance
        Console.WriteLine("\nMoneyMarketAccount: Attempting to withdraw $1000 (maintaining minimum balance)...");
        moneyMarketAccount1.Withdraw(1000);
        Console.WriteLine(moneyMarketAccount1.DisplayAccountInfo());

        // MoneyMarketAccount: Withdraw exceeding minimum balance
        Console.WriteLine("\nMoneyMarketAccount: Attempting to withdraw $1900 (exceeding minimum balance)...");
        moneyMarketAccount1.Withdraw(1900);
        Console.WriteLine(moneyMarketAccount1.DisplayAccountInfo());
    }
}