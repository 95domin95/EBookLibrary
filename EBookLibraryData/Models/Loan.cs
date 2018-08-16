namespace EBookLibraryData.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int PatronId { get; set; }
        public Patron Patron { get; set; }
        public int CopyId { get; set; }
        public Copy Copy { get; set; }
    }
}
