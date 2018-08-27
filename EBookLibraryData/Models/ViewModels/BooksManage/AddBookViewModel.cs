using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class AddBookViewModel
    {
        public int ISBN { get; set; }
        public int Pages { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
