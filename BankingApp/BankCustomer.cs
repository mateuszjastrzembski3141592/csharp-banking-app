using System;

namespace BankingApp;

public partial class BankCustomer : IBankCustomer
{
    private static int s_nextCustomerId;
    private string _firstName = "Tim";
    private string _lastName = "Shao";
    public string CustomerId { get; }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    static BankCustomer()
    {
        Random random = new Random();
        s_nextCustomerId = random.Next(10000000, 20000000);
    }

    public BankCustomer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        CustomerId = s_nextCustomerId++.ToString("D10");
    }

    // Copy constructor for BankCustomer
    public BankCustomer(BankCustomer existingCustomer)
    {
        FirstName = existingCustomer.FirstName;
        LastName = existingCustomer.LastName;
        CustomerId = s_nextCustomerId++.ToString("D10");
    }
}
