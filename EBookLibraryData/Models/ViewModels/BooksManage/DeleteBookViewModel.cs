using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class DeleteBookViewModel
    {
        [Display(Name = "Id Książki")]
        public int Id { get; set; }
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
    }
}
