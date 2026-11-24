using LibraryManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Services.BorrowingPolicies
{
    public class StudentBorrowingPolicy : IBorrowingPolicy
    {
        public int GetBorrowingPeriodDays()
        {
            return 14;
        }

        public int GetMaximumBooks()
        {
            return 3;
        }
    }
}
