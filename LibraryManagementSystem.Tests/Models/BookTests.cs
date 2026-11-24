using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.Models
{
    public class BookTests
    {
        [Fact]
        public void Constructor_CreatesBook_WhenDetailsAreValid()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");

            Assert.Equal(1, book.Id);
            Assert.Equal("Clean Code", book.Title);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Constructor_ThrowsException_WhenIdIsZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Book(0, "Clean Code", "Robert Martin", "123456"));
        }

        [Fact]
        public void Constructor_ThrowsException_WhenTitleIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Book(1, "", "Robert Martin", "123456"));
        }

        [Fact]
        public void Constructor_ThrowsException_WhenAuthorIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Book(1, "Clean Code", "", "123456"));
        }

        [Fact]
        public void Constructor_ThrowsException_WhenISBNIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new Book(1, "Clean Code", "Robert Martin", ""));
        }

        [Fact]
        public void Borrow_MakesBookUnavailable()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");

            book.Borrow();

            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void Borrow_ThrowsException_WhenBookIsUnavailable()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");

            book.Borrow();

            Assert.Throws<InvalidOperationException>(() =>
                book.Borrow());
        }

        [Fact]
        public void Return_MakesBookAvailable()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");

            book.Borrow();
            book.Return();

            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void Return_ThrowsException_WhenBookIsAlreadyAvailable()
        {
            Book book = new Book(1, "Clean Code", "Robert Martin", "123456");

            Assert.Throws<InvalidOperationException>(() =>
                book.Return());
        }
    }
}