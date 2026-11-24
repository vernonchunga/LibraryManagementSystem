using LibraryManagementSystem.Services.BorrowingPolicies;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Tests.Services
{
    public class BorrowingPolicyTests
    {
        [Fact]
        public void StudentMember_ReturnsBorrowingLimit()
        {
            var policy = new StudentBorrowingPolicy();
            var result = policy.GetMaximumBooks();

            Assert.Equal(3, result);
        }

        [Fact]
        public void StudentMember_ReturnsBorrowingPeriod()
        {
            var policy = new StudentBorrowingPolicy();
            var result = policy.GetBorrowingPeriodDays();

            Assert.Equal(14, result);
        }

        [Fact]
        public void RegularMember_ReturnsBorrowingLimit()
        {
            var policy = new RegularBorrowingPolicy();
            var result = policy.GetMaximumBooks();

            Assert.Equal(5, result);
        }

        [Fact]
        public void RegularMember_ReturnsBorrowingPeriod()
        {
            var policy = new RegularBorrowingPolicy();
            var result = policy.GetBorrowingPeriodDays();

            Assert.Equal(21, result);
        }
    }
}
