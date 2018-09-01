using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class AddBookViewModel
    {
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
        [Display(Name = "Ilość Stron")]
        public int Pages { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Ścieżka Do Pliku")]
        public string Path { get; set; }
        [Display(Name = "Wydawca")]
        public string Publisher { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Required]
        public IEnumerable<Category> Categories { get; set; }
    }
}
