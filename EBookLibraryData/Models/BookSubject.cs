using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class BookSubject
    {
        public int BookSubjectId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
