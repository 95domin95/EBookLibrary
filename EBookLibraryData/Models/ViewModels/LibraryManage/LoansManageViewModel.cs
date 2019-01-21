using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class LoansManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "Id",
                    "Id uzytkownika",
                    "Id kopii",
                    "Operacje"
                };
            }
        }
        public int? LoanId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto wypożyczenie";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć wypożyczenia";
            }
        }
        public int Take { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
    }
}
