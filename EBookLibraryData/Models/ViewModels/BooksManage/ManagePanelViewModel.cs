using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.BooksManage
{
    public class ManagePanelViewModel
    {
        public int? Id { get; set; }
        public int OperationType { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public int PagesMin { get; set; }
        public int PagesMax { get; set; }
        public string Path { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
