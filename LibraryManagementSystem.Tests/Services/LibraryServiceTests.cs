using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.Services
{
    public class LibraryServiceTests
    {
        private readonly LibraryService _library = new();

        [Fact]
        public void AddBook_AddsBook_WhenValid()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");

            _library.AddBook(book);

            Assert.Single(_library.GetAllBooks());
        }

        [Fact]
        public void AddBook_ThrowsException_WhenDuplicateId()
        {
            Book first = new(1, "Clean Code", "Robert Martin", "123456");
            Book second = new(1, "Clean Architecture", "Robert Martin", "654321");

            _library.AddBook(first);

            Assert.Throws<InvalidOperationException>(() =>
                _library.AddBook(second));
        }

        [Fact]
        public void AddBook_ThrowsException_WhenDuplicateISBN()
        {
            Book first = new(1, "Clean Code", "Robert Martin", "123456");
            Book second = new(2, "Clean Architecture", "Robert Martin", "123456");

            _library.AddBook(first);

            Assert.Throws<InvalidOperationException>(() =>
                _library.AddBook(second));
        }

        [Fact]
        public void AddBook_ThrowsException_WhenBookIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _library.AddBook(null!));
        }

        [Fact]
        public void AddMember_AddsMember_WhenValid()
        {
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddMember(member);

            Assert.Single(_library.GetAllMembers());
        }

        [Fact]
        public void AddMember_ThrowsException_WhenDuplicateId()
        {
            Member first = new StudentMember(1, "John Banda", "john@gmail.com");
            Member second = new RegularMember(1, "Jane Banda", "jane@gmail.com");

            _library.AddMember(first);

            Assert.Throws<InvalidOperationException>(() =>
                _library.AddMember(second));
        }

        [Fact]
        public void AddMember_ThrowsException_WhenDuplicateEmail()
        {
            Member first = new StudentMember(1, "John Banda", "john@gmail.com");
            Member second = new RegularMember(2, "Jane Banda", "JOHN@GMAIL.COM");

            _library.AddMember(first);

            Assert.Throws<InvalidOperationException>(() =>
                _library.AddMember(second));
        }

        [Fact]
        public void AddMember_ThrowsException_WhenMemberIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _library.AddMember(null!));
        }

        [Fact]
        public void BorrowBook_CreatesLoan_WhenBookAndMemberAreValid()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddBook(book);
            _library.AddMember(member);

            var loan = _library.BorrowBook(1, 1);

            Assert.NotNull(loan);
            Assert.Equal(book, loan.Book);
            Assert.False(book.IsAvailable);
            Assert.Single(_library.GetAllLoans());
        }

        [Fact]
        public void BorrowBook_ThrowsException_WhenBookDoesNotExist()
        {
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");
            _library.AddMember(member);

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook(99, 1));
        }

        [Fact]
        public void BorrowBook_ThrowsException_WhenMemberDoesNotExist()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");
            _library.AddBook(book);

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook(1, 99));
        }

        [Fact]
        public void BorrowBook_ThrowsException_WhenBookIsUnavailable()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddBook(book);
            _library.AddMember(member);

            _library.BorrowBook(1, 1);

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook(1, 1));
        }

        [Fact]
        public void BorrowBook_RejectsStudent_WhenBorrowingLimitIsReached()
        {
            Member student = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddMember(student);

            for (int i = 1; i <= 4; i++)
            {
                Book book = new(i, $"Book {i}", "Author", $"ISBN-{i}");
                _library.AddBook(book);

                if (i <= 3)
                    _library.BorrowBook(i, 1);
            }

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook(4, 1));
        }

        [Fact]
        public void BorrowBook_AllowsBorrowingAgain_AfterPreviousBookIsReturned()
        {
            Member student = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddMember(student);

            for (int i = 1; i <= 3; i++)
            {
                Book book = new(i, $"Book {i}", "Author", $"ISBN-{i}");
                _library.AddBook(book);
                _library.BorrowBook(i, 1);
            }

            _library.ReturnBook(1);

            Book fourthBook = new(4, "Book 4", "Author", "ISBN-4");
            _library.AddBook(fourthBook);

            var loan = _library.BorrowBook(4, 1);

            Assert.NotNull(loan);
            Assert.False(fourthBook.IsAvailable);
        }

        [Fact]
        public void ReturnBook_MakesBookAvailable()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddBook(book);
            _library.AddMember(member);

            var loan = _library.BorrowBook(1, 1);

            _library.ReturnBook(loan.Id);

            Assert.True(book.IsAvailable);
            Assert.True(loan.IsReturned());
        }

        [Fact]
        public void ReturnBook_ThrowsException_WhenLoanDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
                _library.ReturnBook(99));
        }

        [Fact]
        public void ReturnBook_ThrowsException_WhenLoanAlreadyReturned()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddBook(book);
            _library.AddMember(member);

            var loan = _library.BorrowBook(1, 1);

            _library.ReturnBook(loan.Id);

            Assert.Throws<InvalidOperationException>(() =>
                _library.ReturnBook(loan.Id));
        }

        [Fact]
        public void GetBookById_ReturnsCorrectBook()
        {
            Book book = new(1, "Clean Code", "Robert Martin", "123456");

            _library.AddBook(book);

            var result = _library.GetBookById(1);

            Assert.Equal(book, result);
        }

        [Fact]
        public void GetBookById_ReturnsNull_WhenBookDoesNotExist()
        {
            var result = _library.GetBookById(99);

            Assert.Null(result);
        }

        [Fact]
        public void GetMemberById_ReturnsCorrectMember()
        {
            Member member = new StudentMember(1, "John Banda", "john@gmail.com");

            _library.AddMember(member);

            var result = _library.GetMemberById(1);

            Assert.Equal(member, result);
        }

        [Fact]
        public void GetMemberById_ReturnsNull_WhenMemberDoesNotExist()
        {
            var result = _library.GetMemberById(99);

            Assert.Null(result);
        }

        [Fact]
        public void BorrowBook_RejectsRegularMember_WhenBorrowingLimitIsReached()
        {
            Member member = new RegularMember(1, "Jane Banda", "jane@gmail.com");

            _library.AddMember(member);

            for (int i = 1; i <= 6; i++)
            {
                Book book = new(i, $"Book {i}", "Author", $"ISBN-{i}");
                _library.AddBook(book);

                if (i <= 5)
                    _library.BorrowBook(i, 1);
            }

            Assert.Throws<InvalidOperationException>(() =>
                _library.BorrowBook(6, 1));
        }
    }
}