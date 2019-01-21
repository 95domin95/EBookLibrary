using EBookLibraryData;
using EBookLibraryData.Models;
using EBookLibraryData.Models.ViewModels.AccountManage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class UserManageService : IUserManage
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private Context _context;
        public UserManageService(Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<ApplicationUser> GetMany(int take=1000)
        {
            try
            {               
                var users = _context.ApplicationUsers.Take(take);
                if(users != null)
                {
                    return users.ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Lock(string id, int days=10000)
        {
            try
            {
                var users = _context.ApplicationUsers.Where(u => u.Id.Equals(id));
                if (users != null)
                {
                    var user = users.FirstOrDefault();
                    user.LockoutEnd = DateTimeOffset.Now.AddDays(days);
                    user.LockoutEnabled = true;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Unlock(string id)
        {
            try
            {
                var users = _context.ApplicationUsers.Where(u => u.Id.Equals(id));
                if (users != null)
                {
                    var user = users.FirstOrDefault();
                    user.LockoutEnd = DateTimeOffset.Now;
                    user.LockoutEnabled = false;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Remove(string id)
        {
            try
            {
                var user = _context.ApplicationUsers.Where(u => u.Id.Equals(id));
                if(user != null)
                {
                    _context.ApplicationUsers.Remove(user.FirstOrDefault());
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public ApplicationUser GetById(string id)
        {
            try
            {
                var user = _context.ApplicationUsers.Where(u => u.Id.Equals(id));
                if (user != null)
                {
                    return user.FirstOrDefault();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            try
            {
                return _context.Roles;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool SetPassword(ApplicationUser user, string oldPassword, string newPassword)
        {
            try
            {
                if(user != null)
                {
                    var changePasswordresult = _userManager
                        .ChangePasswordAsync(user, oldPassword, newPassword);

                    if(changePasswordresult.IsCompletedSuccessfully)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool SetLoginAndEmail(ApplicationUser user, string email, string userName)
        {
            try
            {
                if (user != null)
                {
                    if(email != null)
                    {
                        user.Email = email;
                        _context.SaveChanges();
                    }

                    if(userName != null)
                    {
                        user.UserName = userName;
                        _context.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
