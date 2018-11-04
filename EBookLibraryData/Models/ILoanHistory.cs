using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface ILoanHistory
    {
        bool AddLoanHistory(Loan loan, ApplicationUser user, Book book);
        bool ModifyReturnDate(DateTime returnDate, LoanHistory loanHistory);
        LoanHistory GetLoanHistory(ApplicationUser user, Book book);
        IEnumerable<LoanHistory> GetAllUserLoanHistories(ApplicationUser user);
        IEnumerable<LoanHistory> GetMany(int take = 1000);
    }
}
