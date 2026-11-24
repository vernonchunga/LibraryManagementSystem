using LibraryManagementSystem.Models;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Services.BorrowingPolicies;

namespace LibraryManagementSystem.Services
{
    public class LibraryService
    {
        private readonly List<Book> _books = new();
        private readonly List<Member> _members = new();
        private readonly List<Loan> _loans = new();

        public void AddBook(Book book)
        {
            ArgumentNullException.ThrowIfNull(book);

            if (_books.Any(b => b.Id == book.Id))
                throw new InvalidOperationException("Book ID already exists.");

            if (_books.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException("ISBN already exists.");

            _books.Add(book);
        }

        public void AddMember(Member member)
        {
            ArgumentNullException.ThrowIfNull(member);

            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException("Member ID already exists.");

            if (_members.Any(m =>
                m.Email.Equals(member.Email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Member email already exists.");

            _members.Add(member);
        }

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public Member? GetMemberById(int id)
        {
            return _members.FirstOrDefault(m => m.Id == id);
        }

        public Loan? GetLoanById(int id)
        {
            return _loans.FirstOrDefault(l => l.Id == id);
        }

        public IReadOnlyList<Book> GetAllBooks()
        {
            return _books.AsReadOnly();
        }

        public IReadOnlyList<Member> GetAllMembers()
        {
            return _members.AsReadOnly();
        }

        public IReadOnlyList<Loan> GetAllLoans()
        {
            return _loans.AsReadOnly();
        }

        private IBorrowingPolicy GetPolicy(Member member)
        {
            return member switch
            {
                StudentMember => new StudentBorrowingPolicy(),
                RegularMember => new RegularBorrowingPolicy(),
                _ => throw new InvalidOperationException("Unknown member type.")
            };
        }

        public Loan BorrowBook(int bookId, int memberId)
        {
            Book? book = GetBookById(bookId);
            Member? member = GetMemberById(memberId);

            if (book == null)
                throw new InvalidOperationException("Book not found.");

            if (member == null)
                throw new InvalidOperationException("Member not found.");

            if (!book.IsAvailable)
                throw new InvalidOperationException("Book is currently unavailable.");

            IBorrowingPolicy policy = GetPolicy(member);

            int activeLoans = _loans.Count(l =>
                l.Member.Id == memberId && !l.IsReturned());

            if (activeLoans >= policy.GetMaximumBooks())
                throw new InvalidOperationException("Member has reached their borrowing limit.");

            int loanId = _loans.Count + 1;

            book.Borrow();

            Loan loan = new Loan(loanId, book, member, policy);

            _loans.Add(loan);

            return loan;
        }

        public void ReturnBook(int loanId)
        {
            Loan? loan = GetLoanById(loanId);

            if (loan == null)
                throw new InvalidOperationException("Loan not found.");

            loan.MarkAsReturned();
        }
    }
}