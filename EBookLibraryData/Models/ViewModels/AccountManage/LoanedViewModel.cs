using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class LoanedViewModel
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
