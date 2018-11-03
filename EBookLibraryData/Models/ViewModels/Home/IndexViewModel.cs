using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<Book> MostRecent { get; set; }
        public IEnumerable<Book> MostPopular { get; set; }
        public bool LoggedOut { get; set; } = false;
    }
}
