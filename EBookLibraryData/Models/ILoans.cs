using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface ILoans
    {
        bool Remove(int id = -1);
        IEnumerable<Loan> GetMany(int take = 1000);
    }
}
