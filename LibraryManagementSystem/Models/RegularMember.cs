using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Models
{
    public class RegularMember : Member
    {
        public RegularMember(int id, string fullName, string email) : base(id, fullName, email)
        { 

        }

        public override int GetBorrowingLimit()
        {
            return 5;
        }

        public override int GetBorrowingPeriodDays()
        {
            return 21;
        }

    }
}
