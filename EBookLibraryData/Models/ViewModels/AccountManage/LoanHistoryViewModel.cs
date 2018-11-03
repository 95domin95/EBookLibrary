using EBookLibraryData.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class LoanHistoryViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "#",
                    "Książka",
                    "Tytuł",
                    "Data wyporzyczenia",
                    "Data zwrotu",
                    "Szczegóły"
                };
            }
        }
        public IEnumerable<LoanHistory> LoanHistories { get; set; }
    }
}
