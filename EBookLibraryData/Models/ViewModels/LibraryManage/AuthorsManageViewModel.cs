using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class AuthorsManageViewModel
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
                    "Operacje"
                };
            }
        }
        public string Name { get; set; }
        public bool AddedSuccessfully { get; set; } = true;
        public string AddSuccess
        {
            get
            {
                return "Dodano autora";
            }
        }
        public string AddFailed
        {
            get
            {
                return "Nie udało się dodać autora";
            }
        }
        public int? AuthorId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = true;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto autora";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć autora";
            }
        }
        public int Take { get; set; }
        public IEnumerable<Author> Authors { get; set; }

    }
}
