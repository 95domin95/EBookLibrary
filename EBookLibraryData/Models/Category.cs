using System.Collections.Generic;

namespace EBookLibraryData.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<BookCategory> BookCategory { get; set; }
    }
}
