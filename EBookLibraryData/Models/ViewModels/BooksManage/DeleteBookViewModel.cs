using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class DeleteBookViewModel
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public bool ById { get; set; }
    }
}
