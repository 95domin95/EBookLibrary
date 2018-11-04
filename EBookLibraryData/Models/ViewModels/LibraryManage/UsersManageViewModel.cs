using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class UsersManageViewModel
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
        public string UserId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto użytkownika";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć użytkownika";
            }
        }
        public int Take { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
