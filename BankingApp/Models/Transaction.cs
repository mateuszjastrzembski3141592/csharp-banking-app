using System;

namespace BankingApp.Models;

// Represents a detailed financial transaction.
public class Transaction
{
    // TODO: Refactor to use auto-properties
    private readonly Guid _transactionId;
    private string _transactionType;
    private DateOnly _transactionDate;
    private TimeOnly _transactionTime;
    private double _transactionAmount;
    private int _sourceAccountNumber;
    private int _targetAccountNumber;
    private string _description;

    public Guid TransactionId => _transactionId;
    public string TransactionType => _transactionType;
    public DateOnly TransactionDate => _transactionDate;
    public TimeOnly TransactionTime => _transactionTime;
    public double TransactionAmount => _transactionAmount;
    public int SourceAccountNumber => _sourceAccountNumber;
    public int TargetAccountNumber => _targetAccountNumber;
    public string Description => _description;

    public Transaction(DateOnly transactionDate, TimeOnly transactionTime, double transactionAmount, int sourceAccountNumber, int targetAccountNumber, string transactionType, string description)
    {
        _transactionId = Guid.NewGuid();
        _transactionDate = transactionDate;
        _transactionTime = transactionTime;
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
        return $"Transaction ID: {_transactionId}, Type: {_transactionType}, Date: {_transactionDate}, Time: {_transactionTime}, Amount: {_transactionAmount}, Source Account: {_sourceAccountNumber}, Target Account: {_targetAccountNumber}, Description: {_description}";
    }
}
