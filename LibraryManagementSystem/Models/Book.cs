using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id {  get; init; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ISBN { get; private set; }

        public bool IsAvailable { get; private set; } = true;

        public Book(int id, string title, string author, string iSBN)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            ArgumentException.ThrowIfNullOrWhiteSpace(author);
            ArgumentException.ThrowIfNullOrWhiteSpace(iSBN);
            
            Id = id;
            Title = title;
            Author = author;
            ISBN = iSBN;
        }

        public void Borrow()
        {
            if (!IsAvailable)
            {
                throw new InvalidOperationException("The book is unavailable");
            }
            IsAvailable = false;
        }

        public void Return()
        {
            if (IsAvailable) 
            { 
                throw new InvalidOperationException("The book has not been borrowed");
            }
            IsAvailable = true;
        }
    }
}
