using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IAuthors
    {
        bool Remove(int id = -1);
        bool Add(string name);
        IEnumerable<Author> GetMany(int take = 1000);
    }
}
