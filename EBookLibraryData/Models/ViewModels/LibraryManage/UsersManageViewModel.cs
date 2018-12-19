using EBookLibraryData.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EBookLibraryData.Models.ViewModels.LibraryManage
{
    public class UsersManageViewModel
    {
        public string[] ColumnNames
        {
            get
            {
                return new string[]
                {
                    "Id",
                    "Nazwa",
                    "Email",
                    "Rola",
                    "Operacje"
                };
            }
        }
        public string RoleChoosed { get; set; } = null;
        public bool RoleChangedSuccessfully { get; set; } = false;
        public bool RoleChangeError { get; set; } = false;
        public string RoleChangeSuccess
        {
            get
            {
                return "Zmieniono rolę użytkownikowi";
            }
        }
        public string RoleChangeFailed
        {
            get
            {
                return "Nie udało się zmienić roli użytkownika";
            }
        }
        public bool UnlockedSuccessfully { get; set; } = false;
        public bool UnlockError { get; set; } = false;
        public string UnlockSuccess
        {
            get
            {
                return "Odblokowano użytkownika";
            }
        }
        public string UnlockFailed
        {
            get
            {
                return "Nie udało się odblokować użytkownika";
            }
        }
        public bool LockedSuccessfully { get; set; } = false;
        public bool LockError { get; set; } = false;
        public string LockSuccess
        {
            get
            {
                return "Zablokowano użytkownika";
            }
        }
        public string LockFailed
        {
            get
            {
                return "Nie udało się zablokować użytkownika";
            }
        }
        public string UserId { get; set; } = null;
        public bool RemovedSuccessfully { get; set; } = false;
        public bool RemoveError { get; set; } = false;
        public string RemoveSuccess
        {
            get
            {
                return "Usunięto użytkownika";
            }
        }
        public string RemoveFailed
        {
            get
            {
                return "Nie udało się usunąć użytkownika";
            }
        }
        public int Take { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

    }
}
