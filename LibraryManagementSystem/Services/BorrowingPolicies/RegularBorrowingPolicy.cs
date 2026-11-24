using LibraryManagementSystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Services.BorrowingPolicies
{
    public class RegularBorrowingPolicy : IBorrowingPolicy
    {
        public int GetMaximumBooks()
        {
            return 5;
        }

        public int GetBorrowingPeriodDays() {
            return 21;
        }
    }
}
