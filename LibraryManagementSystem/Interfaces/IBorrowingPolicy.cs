using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBorrowingPolicy
    {
        int GetMaximumBooks();

        int GetBorrowingPeriodDays();
    }
}
