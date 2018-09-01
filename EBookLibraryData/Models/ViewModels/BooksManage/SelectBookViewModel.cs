using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class SelectBookViewModel
    {
        [Required]
        [Display(Name = "Id Książki")]
        public int? Id { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Display(Name = "Minimalna Ilość Stron")]
        public int PagesMin { get; set; }
        [Display(Name = "Maksymalna Ilość Stron")]
        public int PagesMax { get; set; }
        [Display(Name = "Wydawca")]
        public string Publisher { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
