using System;

namespace BankingApp.Models;

// Represents a detailed financial transaction.
public class Transaction
{
    // TODO: Refactor to use auto-properties
    private Guid _transactionId;
    private string _transactionType;
    private DateOnly _transactionDate;
    private TimeOnly _transactionTime;
    private double _priorBalance;
    private double _transactionAmount;
    private int _sourceAccountNumber;
    private int _targetAccountNumber;
    private string _description;

    public Guid TransactionId
    {
        get => _transactionId;
        set => _transactionId = value;
    }

    public string TransactionType
    {
        get => _transactionType;
        set => _transactionType = value;
    }

    public DateOnly TransactionDate
    {
        get => _transactionDate;
        set => _transactionDate = value;
    }

    public TimeOnly TransactionTime
    {
        get => _transactionTime;
        set => _transactionTime = value;
    }

    public double PriorBalance
    {
        get => _priorBalance;
        set => _priorBalance = value;
    }

    public double TransactionAmount
    {
        get => _transactionAmount;
        set => _transactionAmount = value;
    }

    public int SourceAccountNumber
    {
        get => _sourceAccountNumber;
        set => _sourceAccountNumber = value;
    }

    public int TargetAccountNumber
    {
        get => _targetAccountNumber;
        set => _targetAccountNumber = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public Transaction()
    {
        _transactionType = "";
        _description = "";
    }

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
