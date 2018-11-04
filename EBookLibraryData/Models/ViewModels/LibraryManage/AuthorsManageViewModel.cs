using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Nazwa autora:")]
        public string Name { get; set; }
        public bool AddedSuccessfully { get; set; } = false;
        public bool AddError { get; set; } = false;
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
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
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
