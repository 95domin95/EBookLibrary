﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class LoanHistoriesManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "Id",
                    "Id użytkownika",
                    "Id książki",
                    "Data wypożyczenia",
                    "Data zwrotu",
                    "Operacje"
                };
            }
        }
        public int? LoanHistoryId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto historię wypożczyczeń";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć historii wypożyczeń";
            }
        }
        public int Take { get; set; }
        public IEnumerable<LoanHistory> LoanHistories { get; set; }

    }
}
