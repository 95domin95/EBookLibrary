using EBookLibraryData;
using EBookLibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBookLibraryServices
{
    public class CategoryService: ICategory
    {
        private Context _context;
        public CategoryService(Context context)
        {
            _context = context;
        }
        public bool Remove(int id = -1)
        {
            try
            {
                if (id > 0)
                {
                    var category = _context.Categories.Where(a => a.CategoryId.Equals(id));
                    if (category.Any())
                    {
                        _context.Remove(category.FirstOrDefault());
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Add(string name)
        {

            try
            {
                if (name != null)
                {
                    _context.Categories.Add(new Category
                    {
                        Name = name
                    });
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
        public IEnumerable<Category> GetMany(int take = 1000)
        {
            try
            {
                var categories = _context.Categories.Take(take);
                if (categories.Any())
                {
                    return categories.ToList();
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
