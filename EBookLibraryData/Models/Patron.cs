using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EBookLibraryData.Models
{
    public class Patron : IdentityUser
    {
        public string Adress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
