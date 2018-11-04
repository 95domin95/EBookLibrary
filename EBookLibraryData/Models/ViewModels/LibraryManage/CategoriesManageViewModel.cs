using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class CategoriesManageViewModel
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
        [Display(Name = "Nazwa Kategorii")]
        public string Name { get; set; }
        public bool AddedSuccessfully { get; set; } = false;
        public bool AddError { get; set; } = false;
        public string AddSuccess
        {
            get
            {
                return "Dodano kategorię";
            }
        }
        public string AddFailed
        {
            get
            {
                return "Nie udało się dodać kategorii";
            }
        }
        public int? CategoryId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto kategorię";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć kategorię";
            }
        }
        public int Take { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
