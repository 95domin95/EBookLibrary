namespace EBookLibraryData.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int CopyId { get; set; }
        public Copy Copy { get; set; }
    }
}
