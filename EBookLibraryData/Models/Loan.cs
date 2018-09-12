using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CopyId { get; set; }
        public Copy Copy { get; set; }
    }
}
