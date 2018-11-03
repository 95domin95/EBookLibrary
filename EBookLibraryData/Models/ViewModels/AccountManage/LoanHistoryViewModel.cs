using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class LoanHistoryViewModel
    {
        public IEnumerable<LoanHistory> LoanHistories { get; set; }
    }
}
