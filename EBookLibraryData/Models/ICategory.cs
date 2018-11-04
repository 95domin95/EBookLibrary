using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface ICategory
    {
        bool Remove(int id = -1);
        bool Add(string name);
        IEnumerable<Category> GetMany(int take = 1000);
    }
}
