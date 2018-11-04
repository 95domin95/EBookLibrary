using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IPublishers
    {
        bool Remove(int id = -1);
        bool Add(string name, string city);
        IEnumerable<Publisher> GetMany(int take = 1000);
    }
}
