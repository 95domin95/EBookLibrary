using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public string Path { get; set; }

    }
}
