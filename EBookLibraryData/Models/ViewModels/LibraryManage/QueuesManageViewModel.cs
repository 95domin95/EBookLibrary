using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class QueuesManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "#",
                    "Id",
                    "Id ksiązki",
                    "Id użytkownika",
                    "Data utworzenia",
                    "Operacje"
                };
            }
        }
        public int? QueueId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto kolejkę";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć kolejki";
            }
        }
        public int Take { get; set; }
        public IEnumerable<Queue> Queues { get; set; }

    }
}
