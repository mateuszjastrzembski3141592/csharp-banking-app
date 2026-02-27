using System;

namespace BankingApp.Models;

// Represents a detailed financial transaction.
public class Transaction
{
    // TODO: Refactor to use auto-properties
    private readonly Guid _transactionId;
    private readonly string _transactionType;
    private readonly DateOnly _transactionDate;
    private readonly TimeOnly _transactionTime;
    private readonly double _priorBalance;
    private readonly double _transactionAmount;
    private readonly int _sourceAccountNumber;
    private readonly int _targetAccountNumber;
    private readonly string _description;

    public Guid TransactionId => _transactionId;
    public string TransactionType => _transactionType;
    public DateOnly TransactionDate => _transactionDate;
    public TimeOnly TransactionTime => _transactionTime;
    public double PriorBalance => _priorBalance;
    public double TransactionAmount => _transactionAmount;
    public int SourceAccountNumber => _sourceAccountNumber;
    public int TargetAccountNumber => _targetAccountNumber;
    public string Description => _description;

    public Transaction(DateOnly transactionDate, TimeOnly transactionTime, double priorBalance, double transactionAmount, int sourceAccountNumber, int targetAccountNumber, string transactionType, string description = "")
    {
        _transactionId = Guid.NewGuid();
        _transactionDate = transactionDate;
        _transactionTime = transactionTime;
        _priorBalance = priorBalance;
        _transactionAmount = transactionAmount;
        _sourceAccountNumber = sourceAccountNumber;
        _targetAccountNumber = targetAccountNumber;
        _transactionType = transactionType;
        _description = description;
    }

    // Method to determine whether the transaction is valid based on type and details
    public bool IsValidTransaction()
    {
        if (_transactionAmount <= 0 && _sourceAccountNumber == _targetAccountNumber && _transactionType == "Withdraw")
        {
            return true;
        }
        else if (_transactionAmount > 0 && _sourceAccountNumber == _targetAccountNumber && _transactionType == "Deposit")
        {
            return true;
        }
        else if (_transactionAmount > 0 && _sourceAccountNumber != _targetAccountNumber && _transactionType == "Transfer")
        {
            return true;
        }
        else if (_transactionAmount < 0 && _sourceAccountNumber == _targetAccountNumber && _transactionType == "Bank Fee")
        {
            return true;
        }
        else if (_transactionAmount > 0 && _sourceAccountNumber == _targetAccountNumber && _transactionType == "Bank Refund")
        {
            return true;
        }

        return false;
    }

    // Method to return a formatted string with transaction details
    public string ReturnTransaction()
    {
        return $"Transaction ID: {TransactionId}, Type: {TransactionType}, Date: {TransactionDate}, Time: {TransactionTime}, Prior Balance: {PriorBalance:C}, Amount: {TransactionAmount}, Source Account: {SourceAccountNumber}, Target Account: {TargetAccountNumber}, Description: {Description}";
    }
}
