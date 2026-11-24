using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

LibraryService library = new LibraryService();

bool running = true;

while (running)
{
    Console.WriteLine("\n===== LIBRARY MANAGEMENT SYSTEM =====");
    Console.WriteLine("1. Add Book");
    Console.WriteLine("2. Register Member");
    Console.WriteLine("3. View Books");
    Console.WriteLine("4. View Members");
    Console.WriteLine("5. Borrow Book");
    Console.WriteLine("6. Return Book");
    Console.WriteLine("7. View Loans");
    Console.WriteLine("0. Exit");

    Console.Write("\nSelect an option: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                AddBook();
                break;

            case "2":
                RegisterMember();
                break;

            case "3":
                ViewBooks();
                break;

            case "4":
                ViewMembers();
                break;

            case "5":
                BorrowBook();
                break;

            case "6":
                ReturnBook();
                break;

            case "7":
                ViewLoans();
                break;

            case "0":
                running = false;
                Console.WriteLine("Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void AddBook()
{
    Console.Write("Book ID: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.Write("Title: ");
    string title = Console.ReadLine()!;

    Console.Write("Author: ");
    string author = Console.ReadLine()!;

    Console.Write("ISBN: ");
    string isbn = Console.ReadLine()!;

    Book book = new Book(id, title, author, isbn);

    library.AddBook(book);

    Console.WriteLine("Book added successfully.");
}

void RegisterMember()
{
    Console.Write("Member ID: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.Write("Full name: ");
    string name = Console.ReadLine()!;

    Console.Write("Email: ");
    string email = Console.ReadLine()!;

    Console.WriteLine("1. Student");
    Console.WriteLine("2. Regular Member");

    Console.Write("Select member type: ");
    string? type = Console.ReadLine();

    Member member = type switch
    {
        "1" => new StudentMember(id, name, email),
        "2" => new RegularMember(id, name, email),
        _ => throw new ArgumentException("Invalid member type.")
    };

    library.AddMember(member);

    Console.WriteLine("Member registered successfully.");
}

void ViewBooks()
{
    var books = library.GetAllBooks();

    if (!books.Any())
    {
        Console.WriteLine("No books registered.");
        return;
    }

    foreach (var book in books)
    {
        Console.WriteLine(
            $"ID: {book.Id} | Title: {book.Title} | Author: {book.Author} | ISBN: {book.ISBN} | Available: {book.IsAvailable}");
    }
}

void ViewMembers()
{
    var members = library.GetAllMembers();

    if (!members.Any())
    {
        Console.WriteLine("No members registered.");
        return;
    }

    foreach (var member in members)
    {
        Console.WriteLine(
            $"ID: {member.Id} | Name: {member.FullName} | Email: {member.Email} | Type: {member.GetType().Name}");
    }
}

void BorrowBook()
{
    Console.Write("Book ID: ");
    int bookId = int.Parse(Console.ReadLine()!);

    Console.Write("Member ID: ");
    int memberId = int.Parse(Console.ReadLine()!);

    Loan loan = library.BorrowBook(bookId, memberId);

    Console.WriteLine("Book borrowed successfully.");
    Console.WriteLine($"Loan ID: {loan.Id}");
    Console.WriteLine($"Borrow date: {loan.BorrowDate}");
    Console.WriteLine($"Due date: {loan.DueDate}");
}

void ReturnBook()
{
    Console.Write("Loan ID: ");
    int loanId = int.Parse(Console.ReadLine()!);

    library.ReturnBook(loanId);

    Console.WriteLine("Book returned successfully.");
}

void ViewLoans()
{
    var loans = library.GetAllLoans();

    if (!loans.Any())
    {
        Console.WriteLine("No borrowing records available.");
        return;
    }

    foreach (var loan in loans)
    {
        Console.WriteLine(
            $"Loan ID: {loan.Id} | Book: {loan.Book.Title} | Member: {loan.Member.FullName} | Borrowed: {loan.BorrowDate} | Due: {loan.DueDate} | Returned: {loan.IsReturned()} | Overdue: {loan.IsOverdue()}");
    }
}