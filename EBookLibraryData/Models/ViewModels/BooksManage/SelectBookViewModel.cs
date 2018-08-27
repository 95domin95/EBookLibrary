using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class SelectBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public int PagesMin { get; set; }
        public int PagesMax { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public bool ById { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
