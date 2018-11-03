﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }
        public string City { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public string PostalCode { get; set; }
        public long BooksRead { get; set; } = 0;
        public ICollection<Loan> Loans { get; set; }
        public ICollection<Queue> Queues { get; set; }
    }
}
