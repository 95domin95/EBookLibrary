using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublisherId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        //public int CountrId { get; set; }
        //[ForeignKey("CountryId")]
        public Country Country { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
