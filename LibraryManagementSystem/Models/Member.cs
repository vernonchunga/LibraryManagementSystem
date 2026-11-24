using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public int Id { get; init; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public Member(int id, string fullName, string email)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
        }

        public virtual int GetBorrowingLimit()
        {
            return 0;
        }

        public virtual int GetBorrowingPeriodDays()
        {
            return 0;
        }

    }
}
