# csharp-banking-app
This project was built while working through the Microsoft Learn quickstart exercises on developing object-oriented applications in C#.

Rather than completing each exercise in separate module folders, the application was built as a single evolving project to better reflect real-world development practices and maintain a continuous Git history.

## Purpose
The repository is used to strengthen my understanding of:

- C#  
- .NET  
- Object-Oriented Programming (OOP)  
- Git and GitHub workflows  

## Source
https://microsoftlearning.github.io/mslearn-develop-oop-csharp/

## Status
Active learning project.

## Development Roadmap
| Exercise | Description |
| :--- | :--- |
| **Ex 1: Create class definitions and instantiate objects** | - Created `BankCustomer` class structure.<br>- Created `BankAccount` class structure.<br>- Implemented basic Console test in `Program.cs`. |
| **Ex 2: Update a class with properties and methods** | - Encapsulated data using properties in `BankCustomer` and `BankAccount`.<br>- Implemented basic methods to change and display customer info in `BankCustomer`.<br>- Implemented core banking methods in `BankAccount`.<br>- Created extension methods in `Extensions.cs` for `BankCustomer` and `BankAccount`.<br>- Updated `Program.cs` to test the new logic. |
| **Ex 3: Manage class implementations** | - Refactored `BankCustomer` into  partial classes (`BankCustomerMethods`).<br>- Added static properties for `BankAccount`.<br>- Created static `AccountCalculations` class for BankAccount methods.<br>-  Added optional parameters to `BankAccount` instance constructor.<br>- Implemented copy constructors for `BankCustomer` and `BankAccount`.<br>- Refactored `Program.cs` to test new functionalities and used object initializers. |
| **Supplement to Ex 7** | - Created `IBankAccount` and `IBankCustomer` interfaces.<br>- Updated `BankAccount` to implement `IBankAccount` interface.<br>- Updated `BankCustomer` and `BankCustomerMethods` to implement `IBankCustomer` interface. |
| **Ex 7: Implement base and derived classes** | - Created `CheckingAccount`, `SavingsAccount`, and `MoneyMarketAccount` derived classes.<br>- Updated `BankAccount` class definition with the `virtual` keyword.<br>- Implemented specialized features in derives classes.<br>- Updated derived classes to override properties and methods.<br>- Refactored `Program.cs` to test new functionalities. |
| **Ex 8: Implement polymorphism** | - Created `IMonthlyReportGenerator`, `IQuarterlyReportGenerator`, and `IYearlyReportGenerator` reporting interfaces.<br>- Created `AccountReportGenerator` and `CustomerReportGenerator` reporting classes.<br>- Refactored project structure into `Interfaces`, `Models`, and `Services` categories.<br>- Refactored `Program.cs` to test new functionalities.|
| **Supplement to EX 9** | - `Interfaces`: Updated `IBankCustomer`, `IMonthlyReportGenerator`, `IQuarterlyReportGenerator`, and `IYearlyReportGenerator` interfaces.<br>- `Models`: Created `CertificateOfDepositAccount`, `PremiumCustomer`, `StandardCustomer`, and `Transaction` classes.<br>- `Models`: Updated `BankAccount`, `BankCustomer`, `CheckingAccount`, `MoneyMarketAccount`, and `SavingsAccount` classes.<br>- `Services`: Created `SimulateTransaction` class.<br>- `Services`: Updated `AccountReportGenerator` and `CustomerReportGenerator` classes.<br>- Refactored `Program` as preparation for `Ex 9`.|
| **Ex 9: Implement basic Date and Time operations** |- Refactored `Program` to demonstrate the usage of Date & Time operations and test new functionalities|
| **Supplement to EX 10** | - `Interfaces`: Updated `IBankAccount` and `IBankCustomer` interfaces.<br>- `Models`: Created `Bank` class.<br>- `Models`: Refactored classes.<br>- `Services`: Created `SimulateDepositsWithdrawalsTransfers`.<br>- `Services`: Updated `SimulateTransactions`.<br>- Refactored `Program` as preparation for `EX 10`. |
| **Ex 10: Implement collection types** | - Updated `IBankAccount` and `IBankCustomer` interfaces.<br>- Updated `Bank`, `BankAccount`, `BankCustomer`, `CheckingAccount`, and `SimulateDepositsWithdrawalsTransfers`.<br>- Updated `Program` to test new functionalities with usage of `HashSet` and `Dictionary`. |