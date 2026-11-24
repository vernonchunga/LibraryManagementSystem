using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Models
{
    public class Loan
    {
        public int Id { get; init; }
        public Book Book { get; private set; }
        public Member Member { get; private set; }
        public DateTime BorrowDate { get; private set; } = DateTime.Now;
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        private readonly IBorrowingPolicy _borrowingPolicy;

        public Loan(int id, Book book, Member member, IBorrowingPolicy borrowingPolicy)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            ArgumentNullException.ThrowIfNull(book);
            ArgumentNullException.ThrowIfNull(member);
            ArgumentNullException.ThrowIfNull(borrowingPolicy);

            Id = id;
            Book = book;
            Member = member;
            _borrowingPolicy = borrowingPolicy;

            CalculateDueDate();
        }

        private void CalculateDueDate()
        {
            int period = _borrowingPolicy.GetBorrowingPeriodDays();

            if (period <= 0)
                throw new InvalidOperationException("Borrowing period must be positive.");

            DueDate = BorrowDate.AddDays(period);
        }

        public bool IsOverdue()
        {
            return ReturnDate == null && DateTime.Now > DueDate;
        }

        public bool IsReturned()
        {
            return ReturnDate.HasValue;
        }

        public void MarkAsReturned()
        {
            if (IsReturned())
                throw new InvalidOperationException("This loan has already been returned.");

            Book.Return();
            ReturnDate = DateTime.Now;
        }
    }
}