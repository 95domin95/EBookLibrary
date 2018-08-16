using System.Collections.Generic;

namespace EBookLibraryData.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Path { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookCategory> BookCategory { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
