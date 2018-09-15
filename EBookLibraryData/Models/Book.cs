using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public int? ISBN { get; set; }
        public string Title { get; set; }
        public int? Pages { get; set; }
        public string Path { get; set; }
        public string CoveringPath { get; set; }
        public string Author { get; set; }
        public int? PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
