using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models
{
    public interface IUserManage
    {
        bool Lock(string id, int days=10000);
        bool Unlock(string id);
        bool Remove(string id);
        IEnumerable<ApplicationUser> Users(int take = 1000);
    }
}
