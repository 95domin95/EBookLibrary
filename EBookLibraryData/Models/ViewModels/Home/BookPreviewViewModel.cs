using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class BookPreviewViewModel : LoanViewModel
    {
        public Book Book { get; set; }
        public bool BookRent { get; set; } = false; //If book was rented by user previously
    }
}
