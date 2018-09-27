using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Shared
{
    public class Pagination
    {
        [RegularExpression("([0-9]+)")]
        public int Page { get; set; } = 1;
        public int ElementsOnPage { get; set; } = 12;
        public int AllPagesCount { get; set; } = 0;
        public bool AnyElements { get; set; } = true;
        public bool MoreThanOnePage { get; set; } = true;
        public string NoElementsMessage { get; set; } = "Brak elementów do wyświetlenia";
    }
}
