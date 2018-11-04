using EBookLibraryData;
using EBookLibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class UserManageService : IUserManage
    {
        private Context _context;
        public UserManageService(Context context)
        {
            _context = context;
        }
        public IEnumerable<ApplicationUser> Users(int take=1000)
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
    }
}
