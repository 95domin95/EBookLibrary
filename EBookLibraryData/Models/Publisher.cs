using System.Collections.Generic;

namespace EBookLibraryData.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<Book> Books { get; set; }
    }
}
