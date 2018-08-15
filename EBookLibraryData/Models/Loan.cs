using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int PatronId { get; set; }
        public Patron Patron { get; set; }
    }
}
