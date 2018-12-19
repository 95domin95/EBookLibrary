using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class LoanViewModel
    {
        private DateTime _endDate = DateTime.Now.AddDays(7);
        public int? UserId { get; set; } = null;
        public int? BookId { get; set; } = null;
        public string LoanSuccessMessage { get; } = "Wyporzyczyłeś e-booka!";
        public string LoanErrorMessage { get; } = "Wystąpił problem przy realizacji zlecenia.";
        public string BookNotAvailableMessage { get; set; } = "Książka aktualnie niedostępna.";
        [Display(Name = "Data zwrotu:")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate
        {
            get
            {
                return this._endDate;
            }
            set
            {
                if(Convert.ToDateTime(value) > DateTime.Now.AddDays(7))
                {
                    this._endDate = DateTime.Now.AddDays(7);
                }
                else
                {
                    this._endDate = value;
                }
            }
        }
        public bool LoanError { get; set; } = false;
        public bool BookNotAvailable { get; set; } = false;
        public bool BookRented { get; set; } = false; //If user rented book now
    }
}
