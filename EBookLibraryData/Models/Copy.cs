using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Copy
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Patronid { get; set; }
        public Patron Patron { get; set; }
    }
}
