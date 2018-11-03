using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EBookLibraryData.Models
{
    public class LoanHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanHistoryId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public int? BookId { get; set; }//?int dla systuacji w której książka zostanie usunięta z biblioteki z jakiegoś powodu
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
