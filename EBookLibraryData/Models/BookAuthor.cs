using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class BookAuthor
    {
        public int BookAuthorId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
