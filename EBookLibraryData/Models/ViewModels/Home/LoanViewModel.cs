using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class LoanViewModel
    {
        public int? UserId { get; set; } = null;
        public int? BookId { get; set; } = null;
        public string LoanSuccessMessage { get; } = "Wyporzyczyłeś e-booka!";
        public string LoanErrorMessage { get; } = "Wystąpił problem przy realizacji zlecenia.";
        public string BookNotAvailableMessage { get; set; } = "Książka aktualnie niedostępna.";
        [Display(Name = "Ilość dni")]
        public int LoanPeroidDays { get; set; } = 7;
        public bool LoanError { get; set; } = false;
        public bool BookNotAvailable { get; set; } = false;
        public bool BookRented { get; set; } = false; //If user rented book now
    }
}
