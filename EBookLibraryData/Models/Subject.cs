using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int BookSubjectId { get; set; }
        public BookSubject BookSubject { get; set; }
    }
}
