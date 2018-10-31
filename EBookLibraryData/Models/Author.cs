using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
