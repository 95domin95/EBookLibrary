using EBookLibraryData.Models.ViewModels.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class ManagePanelViewModel : Pagination
    {
        public List<string> Errors = new List<string>
        {
            "add",
            "remove",
            "modify"
        };
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
        [Display(Name = "Okładka")]
        public IFormFile BookCovering { get; set; }
        [Display(Name = "Książka")]
        public bool BookSearched { get; set; } = false;
        public bool BookModified { get; set; } = false;
        public bool BookRemoved { get; set; } = false;
        public bool BookAdded { get; set; } = false;
        public string OperationErrorName { get; set; } = string.Empty;
        public string BookAddedMessageSuccess { get; set; } = "Nowa książka została dodana.";
        public string BookRemovedMessageSuccess { get; set; } = "Książka została usunięta";
        public string BookModifiedMessageSuccess { get; set; } = "Dane książki zostały zmienione.";
        public string BookAddedMessageError { get; set; } = "Nie udało się dodać książki o podanych paramaterach.";
        public string BookRemovedMessageError { get; set; } = "Nie udało się usunąć książki.";
        public string BookModifiedMessageError { get; set; } = "Dane książki nie zostały zmienione.";
        public IFormFile Book { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<string[]> Operations { get; set; }
    }
}
