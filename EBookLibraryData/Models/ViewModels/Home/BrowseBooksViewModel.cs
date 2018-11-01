using EBookLibraryData.Models.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class BrowseBooksViewModel : Pagination
    {
        [Display(Name = "ISBN")]
        [RegularExpression("([0-9]+)")]
        public int? ISBN { get; set; }
        [Display(Name = "Tytuł")]
        [MaxLength(150)]
        public string Title { get; set; }
        [Display(Name = "Ilość Stron")]
        [RegularExpression("([0-9]+)")]
        public int? Pages { get; set; }
        [Display(Name = "Minimalna Ilość Stron")]
        [RegularExpression("([0-9]+)")]
        public int? PagesMin { get; set; }
        [Display(Name = "Maksymalna Ilość Stron")]
        [RegularExpression("([0-9]+)")]
        public int? PagesMax { get; set; }
        [Display(Name = "Wydawca")]
        [MaxLength(150)]
        public string Publisher { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        public bool PagesRangeSelected { get; set; } = false;
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [Display(Name = "Autorzy")]
        public IEnumerable<Author> Authors { get; set; }
    }
}
