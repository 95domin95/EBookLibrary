namespace EBookLibraryData.Models
{
    public class Copy
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
