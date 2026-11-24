# Library Management System

## Overview

The Library Management System is a console-based application developed using C# and .NET. It provides basic library management functionality, allowing librarians to register books and members, manage borrowing operations, process returns and track loan information.

The application demonstrates Object-Oriented Programming (OOP) principles, including encapsulation, inheritance, abstraction and polymorphism.

## Features

- Register new books.
- Register student and regular members.
- Prevent duplicate book IDs and ISBN numbers.
- Prevent duplicate member IDs and email addresses.
- View registered books and members.
- Search for books and members using their IDs.
- Borrow available books.
- Enforce different borrowing limits for student and regular members.
- Automatically calculate borrowing due dates.
- Process book returns.
- Track borrowing records.
- Identify overdue loans.
- Validate input and handle application errors.

## Technologies Used

- C#
- .NET 10
- xUnit
- Visual Studio
- Git and GitHub

## Borrowing Rules

The application implements different borrowing policies for different member categories.

| Member Type | Maximum Books | Borrowing Period |
|---|---:|---:|
| Student Member | 3 | 14 days |
| Regular Member | 5 | 21 days |

Members cannot borrow unavailable books or exceed their borrowing limits.

Returning a book makes it available for another member to borrow.

## Project Structure

```text
LibraryManagementSystem/
│
├── LibraryManagementSystem/
│   ├── Models/
│   ├── Interfaces/
│   ├── Services/
│   │   └── BorrowingPolicies/
│   └── Program.cs
│
├── LibraryManagementSystem.Tests/
│   ├── Models/
│   └── Services/
│
├── LibraryManagementSystem.sln
├── README.md
└── .gitignore
```

## Architecture and Design

The application follows a simple separation of responsibilities.

### Models

The Models folder contains the application's core entities:

- Book: Represents a library book and manages its availability.
- Member: Represents a registered library member.
- StudentMember: Represents a student member.
- RegularMember: Represents a regular member.
- Loan: Represents a borrowing transaction, including borrowing dates, due dates and return information.

### Interfaces

The Interfaces folder contains `IBorrowingPolicy`, which defines the contract for borrowing policies.

### Services

The Services folder contains `LibraryService`, which manages book and member registration, retrieval, borrowing and returning operations.

The BorrowingPolicies folder contains the implementations of the borrowing rules:

- StudentBorrowingPolicy
- RegularBorrowingPolicy

### Tests

The test project uses xUnit to verify model behaviour, validation rules, borrowing policies and library operations.

## Object-Oriented Programming Principles

The project demonstrates the following OOP principles:

**1. Encapsulation**

Book and Loan objects control their internal state through methods rather than exposing public setters. For example, a book's availability is changed through its Borrow() and Return() methods.

**2. Inheritance**

StudentMember and RegularMember inherit from the Member base class, allowing common member properties and behaviour to be reused.

**3. Abstraction**

The IBorrowingPolicy interface defines the operations that borrowing policies must implement without specifying their individual rules.

**4. Polymorphism**

Different member types are handled through the common Member type. The application identifies their specific borrowing policies at runtime.

## Requirements

To run the application, you need:

- .NET 10 SDK or a compatible development environment.
- Visual Studio or another .NET-compatible IDE.

## Running the Application

Clone the repository:

```bash
git clone https://github.com/vernonchunga/LibraryManagementSystem.git
```

Navigate into the solution directory:

```bash
cd LibraryManagementSystem
```

Build the solution:

```bash
dotnet build
```

Run the application:

```bash
dotnet run --project LibraryManagementSystem
```

## Running Automated Tests

Run the complete test suite from the solution directory:

```bash
dotnet test
```

The tests cover:

- Book creation and validation.
- Book borrowing and returning.
- Loan creation and due-date calculations.
- Borrowing policy rules.
- Book and member registration.
- Duplicate registration prevention.
- Borrowing limits.
- Book availability.
- Loan retrieval and return operations.

## Assumptions and Limitations

1. The application uses in-memory collections to store books, members and loans.
2. Application data is lost when the program closes.
3. Each book and member must have a unique ID.
4. Each book must have a unique ISBN.
5. Each member must have a unique email address.
6. Student members can borrow a maximum of three books for fourteen days.
7. Regular members can borrow a maximum of five books for twenty-one days.
8. The application uses the system's local date and time when recording borrowing and return dates.
9. The application is designed for console-based interaction and does not include a graphical user interface.
10. The current implementation does not include database persistence, authentication or multiple librarian accounts.

## Future Improvements

Potential future enhancements include:

- Database integration using SQL Server or PostgreSQL.
- Persistent storage of books, members and borrowing records.
- Graphical user interface or web-based frontend.
- Authentication and role-based access control.
- Fine calculation for overdue books.
- Book reservations.
- Search and filtering functionality.
- Detailed borrowing and membership reports.

## Author

Developed as a C# Object-Oriented Programming and Software Development project.