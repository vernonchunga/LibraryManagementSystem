using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.BorrowingPolicies;
using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Tests.Models
{
    public class LoanTests
    {
        [Fact]
        public void Constructor_CreatesLoan_WhenDetailsAreValid()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");
            IBorrowingPolicy policy = new StudentBorrowingPolicy();

            Loan loan = new Loan(1, book, member, policy);

            Assert.Equal(1, loan.Id);
            Assert.Equal(book, loan.Book);
            Assert.Equal(member, loan.Member);
        }

        [Fact]
        public void Constructor_SetsCorrectDueDate_ForStudent()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            Loan loan = new Loan(1, book, member, new StudentBorrowingPolicy());

            Assert.Equal(loan.BorrowDate.AddDays(14), loan.DueDate);
        }

        [Fact]
        public void Constructor_SetsCorrectDueDate_ForRegularMember()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new RegularMember(1, "Jane Banda", "jane@gmail.com");

            Loan loan = new Loan(1, book, member, new RegularBorrowingPolicy());

            Assert.Equal(loan.BorrowDate.AddDays(21), loan.DueDate);
        }

        [Fact]
        public void Constructor_ThrowsException_WhenPolicyIsNull()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            Assert.Throws<ArgumentNullException>(() =>
                new Loan(1, book, member, null!));
        }

        [Fact]
        public void NewLoan_IsNotReturned()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            Loan loan = new Loan(1, book, member, new StudentBorrowingPolicy());

            Assert.False(loan.IsReturned());
        }

        [Fact]
        public void NewLoan_IsNotOverdue()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            Loan loan = new Loan(1, book, member, new StudentBorrowingPolicy());

            Assert.False(loan.IsOverdue());
        }

        [Fact]
        public void MarkAsReturned_RecordsReturnDate()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            book.Borrow();

            Loan loan = new Loan(1, book, member, new StudentBorrowingPolicy());

            loan.MarkAsReturned();

            Assert.True(loan.IsReturned());
            Assert.NotNull(loan.ReturnDate);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void MarkAsReturned_ThrowsException_WhenAlreadyReturned()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            book.Borrow();

            Loan loan = new Loan(1, book, member, new StudentBorrowingPolicy());

            loan.MarkAsReturned();

            Assert.Throws<InvalidOperationException>(() =>
                loan.MarkAsReturned());
        }
    }
}