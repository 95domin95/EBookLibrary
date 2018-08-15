using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
