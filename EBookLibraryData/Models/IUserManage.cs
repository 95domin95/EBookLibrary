using Microsoft.AspNetCore.Identity;
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
        IEnumerable<ApplicationUser> GetMany(int take = 1000);
        ApplicationUser GetById(string id);
        IEnumerable<IdentityRole> GetAllRoles();
    }
}
