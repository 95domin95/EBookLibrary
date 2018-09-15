using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class FileDetails
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
    public class ManagePanelViewModel
    {
        [RegularExpression("([0-9]+)")]
        [Display(Name = "Id Książki")]
        public int? Id { get; set; }
        [Display(Name = "Typ Operacji")]
        public string OperationType { get; set; }
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
        public List<FileDetails> Files { get; set; }
            = new List<FileDetails>();
        [Display(Name = "Okładka")]
        public IFormFile BookCovering { get; set; }
        [Display(Name = "Książka")]
        public IFormFile Book { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<string[]> Operations { get; set; }
    }
}
