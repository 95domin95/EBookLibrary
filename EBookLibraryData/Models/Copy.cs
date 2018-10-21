﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookLibraryData.Models
{
    public class Copy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public bool IsRented { get; set; } = false;
    }
}
