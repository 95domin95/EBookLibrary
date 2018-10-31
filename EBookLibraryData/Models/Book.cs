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
        public int CopiesCount { get; set; } = 1;
        public string Path { get; set; }
        public string CoveringPath { get; set; }
        public int? PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<Copy> Copies { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
