using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Models
{
    public class StudentMember : Member
    {
        public StudentMember(int id, string fullName, string email) : base (id, fullName, email) 
        {
        }

        public override int GetBorrowingLimit()
        {
            return 3;
        }

        public override int GetBorrowingPeriodDays()
        {
            return 14;
        }
    }
}
