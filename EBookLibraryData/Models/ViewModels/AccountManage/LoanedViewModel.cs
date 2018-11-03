using EBookLibraryData.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.AccountManage
{
    public class LoanedViewModel
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
                    "Szczegóły"
                };
            }
        }
        public IEnumerable<Book> Books { get; set; }
    }
}
