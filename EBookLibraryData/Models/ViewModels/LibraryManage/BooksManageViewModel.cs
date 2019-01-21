using EBookLibraryData.Models.ViewModels.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class BooksManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "Okładka",
                    "Id",
                    "Tytuł",
                    "Ilość wypożyczeń",
                    "Ilość kopii",
                    "Opcje"
                };
            }
        }
        [RegularExpression("([0-9]+)")]
        [Display(Name = "Id Książki")]
        public int? Id { get; set; } = null;
        [Display(Name = "Ilość kopii")]
        [RegularExpression("([0-9]+)")]
        public int? CopiesCount { get; set; } = null;
        [Display(Name = "Dostępne")]
        public bool? Availability { get; set; } = false;
        [Display(Name = "ISBN")]
        [RegularExpression("([0-9]+)")]
        public int? ISBN { get; set; } = null;
        [Display(Name = "Tytuł")]
        [MaxLength(150)]
        public string Title { get; set; } = null;
        [Display(Name = "Ilość Stron")]
        [RegularExpression("([0-9]+)")]
        public int? Pages { get; set; } = null;
        [Display(Name = "Wydawca")]
        public string Publisher { get; set; } = null;
        [Display(Name = "Kategoria")]
        public string Category { get; set; } = null;
        [Display(Name = "Autor")]
        public string[] Author { get; set; } = null;
        [Display(Name = "Książka")]
        public IFormFile Book { get; set; } = null;
        [Display(Name = "Okładka")]
        public IFormFile BookCovering { get; set; } = null;
        [Display(Name = "Elementów do wyświetlenia")]
        public int ElementsToshow { get; set; } = 1000;
        public bool AddedSuccessfully { get; set; } = false;
        public bool AddError { get; set; } = false;
        public string AddSuccess
        {
            get
            {
                return "Dodano książkię";
            }
        }
        public string AddFailed
        {
            get
            {
                return "Nie udało się dodać książki";
            }
        }
        public int? CategoryId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto książkę";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć książki";
            }
        }

        public bool ModifiedSuccessfully { get; set; } = false;
        public bool ModifiedError { get; set; } = false;
        public string ModifiedSuccess
        {
            get
            {
                return "Zmieniono dane książki";
            }
        }
        public string ModifiyFailed
        {
            get
            {
                return "Nie udało się zmienić danych książki";
            }
        }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [Display(Name = "Wybierz autora")]
        public int Take { get; set; } = 1000;
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}
