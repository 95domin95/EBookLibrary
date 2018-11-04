using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class PublisherManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "#",
                    "Id",
                    "Nazwa",
                    "Miasto",
                    "Operacje"
                };
            }
        }
        public string Name { get; set; }
        public string City { get; set; }
        public bool AddedSuccessfully { get; set; } = true;
        public string RemoveSuccess
        {
            get
            {
                return "Dodano wydawcę";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się dodać wydawcę";
            }
        }

        public int? PublisherId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = true;
        public string AddSuccess
        {
            get
            {
                return "Usunięto wydawcę";
            }
        }
        public string AddFailed
        {
            get
            {
                return "Nie udało się usunąć wydawcy";
            }
        }
        public int Take { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }

    }
}
