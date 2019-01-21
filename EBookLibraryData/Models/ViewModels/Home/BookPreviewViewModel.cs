using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class BookPreviewViewModel : LoanViewModel
    {
        public Book Book { get; set; }
        public bool BookRent { get; set; } = false; //If book was rented by user previously
        public bool InQueue { get; set; } = false; //If user is waiting in queue for this book
        public bool JoinQueueError = false;
        public string JoinQueueSuccess { get; set; } = "Dołączyłeś do kolejki, gdy zwolni się egzemplarz automatycznie wypożyczysz książkę!";
        public string JoinQueueFailed { get; set; } = "Już oczekujesz na tę książkę";
    }
}
