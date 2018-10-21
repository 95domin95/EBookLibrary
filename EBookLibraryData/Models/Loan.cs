using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int CopyId { get; set; }
        [ForeignKey("CopyId")]
        public Copy Copy { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int LoanDurationDays { get; set; } = 7;
    }
}
